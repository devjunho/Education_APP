#include "clnt_handler.h"

Clnt_handler::Clnt_handler(int sock)
{
    mSock = sock;
    mDB_Account = "Tester";
    mDB_Password = "1234";
}

Clnt_handler::~Clnt_handler() { }

void Clnt_handler::trySignUP(const Data & data, bool isEMP)
{
    Data sendData;

    try
    {
        DB temp;
        temp.setAccount(mDB_Account, mDB_Password);
        sql::Connection* tempCoon = temp.connect();

        // 회원가입 쿼리
        sql::PreparedStatement *Insert = 
            tempCoon->prepareStatement("INSERT INTO USER(USER_ID, PW, NAME, E_MAIL, OPER) VALUES(?, ?, ?, ?, ?)");
        Insert->setString(1, data.User[0]);
        Insert->setString(2, data.User[1]);
        Insert->setString(3, data.User[2]);
        Insert->setString(4, data.User[3]);
        Insert->setInt(5, isEMP);
        Insert->executeQuery();
        sendData.Type = SIGN_UP_SUCCEED;

        // 데이터 확인 ( 완성 후 제거)
        std::cout << "SIGN_UP_SUCCEED\n";
    }
    catch(const std::exception& e)
    {
        // db연결 중에 오류가 날 때 (회원가입 실패)
        std::cerr << "DB 오류 " << e.what() << '\n';
        sendData.Type = SIGN_UP_FAIL;

        // 데이터 확인
        std::cout << "SIGN_UP_FAIL\n";
    }

    // 직렬화 후 SEND
    json js;
    js = json{{"Type", sendData.Type}};
    std::string serializedData = js.dump();
    send(mSock, serializedData.c_str(), serializedData.size(), 0);
}

void Clnt_handler::tryCheckID(const Data & data)
{
    Data sendData;

    try
    {
        DB temp;
        temp.setAccount(mDB_Account, mDB_Password);
        sql::Connection* tempCoon = temp.connect();

        // ID확인 쿼리
        sql::PreparedStatement *idCheck = 
            tempCoon->prepareStatement("SELECT * FROM USER WHERE USER_ID = ?");
        idCheck->setString(1, data.User[0]);
        sql::ResultSet *idRes = idCheck->executeQuery();

        if (idRes->next())
        // 일치하는 id가 있으면
        {
            sendData.Type = SIGN_UP_CHECK_ID_SUCCED;

            // 데이터 확인 
            std::cout << "SIGN_UP_CHECK_ID_SUCCED\n";
        }
        else
        {
            sendData.Type = SIGN_UP_CHECK_ID_FAIL;

            // 데이터 확인 
            std::cout << "SIGN_UP_CHECK_ID_FAIL\n";
        }
    }
    catch(const std::exception& e)
    {
        // db연결 중에 오류가 날 때 (회원가입 실패)
        std::cerr << "DB 오류 " << e.what() << '\n';
        sendData.Type = SIGN_UP_FAIL;

        // 데이터 확인 
        std::cout << "SIGN_UP_FAIL\n";
    }

    // 직렬화 후 SEND
    json js;
    js = json{{"Type", sendData.Type}};
    std::string serializedData = js.dump();
    send(mSock, serializedData.c_str(), serializedData.size(), 0);
}

void Clnt_handler::tryLogin(const Data & data, bool isEMP)
{
    Data sendData;

    try
    {
        DB temp;
        temp.setAccount(mDB_Account, mDB_Password);
        sql::Connection* tempCoon = temp.connect();

        // ID확인 쿼리
        sql::PreparedStatement *idCheck = 
            tempCoon->prepareStatement("SELECT * FROM USER WHERE USER_ID = ?");
        idCheck->setString(1, data.User[0]);
        sql::ResultSet *idRes = idCheck->executeQuery();

        if (!idRes->next())
        // 일치하는 id가 없으면
        {
            sendData.Type = LOGIN_FAIL_ID;
            json js;
            js = json{{"Type", sendData.Type}};
            std::string serializedData = js.dump();
            send(mSock, serializedData.c_str(), serializedData.size(), 0);

            std::cout << "LOGIN_FAIL_ID\n";
            return;
        }

        // PW, EMP확인 쿼리
        sql::PreparedStatement *pwCheck = 
            tempCoon->prepareStatement("SELECT * FROM USER WHERE USER_ID = ? AND PW = ? AND OPER = ?");
        pwCheck->setString(1, data.User[0]);
        pwCheck->setString(2, data.User[1]);
        pwCheck->setInt(3, isEMP);
        sql::ResultSet *pwRes = pwCheck->executeQuery();

        if (pwRes->next())
        // 로그인 성공
        {
            sendData.Type = LOGIN_SUCCEED;

            // 데이터 확인
            std::cout << "LOGIN_SUCCEED\n";
        }
        else
        // 로그인 실패
        {
            sendData.Type = LOGIN_FAIL_PW;

            // 데이터 확인
            std::cout << "LOGIN_FAIL_PW\n";
        }
    }
    catch(const std::exception& e)
    {
        // db연결 중에 오류가 날 때 (로그인 실패)
        std::cerr << "DB 오류 " << e.what() << '\n';
        sendData.Type = LOGIN_FAIL;

        // 데이터 확인
        std::cout << "LOGIN_FAIL\n";
    }

    // 직렬화 후 SEND
    json js;
    js = json{{"Type", sendData.Type}};
    std::string serializedData = js.dump();
    send(mSock, serializedData.c_str(), serializedData.size(), 0);
}

void Clnt_handler::trySearch(const Data & data)
{
    CURL* curl;
    CURLcode res;
    std::string readBuffer;
    Data sendData;
    json js;
    std::string serializedData;

    // 검색 요청 처리 
    std::string srchScnm = data.Plant[2];
    std::string srchKrnm = data.Plant[0];
    std::string srchFamlNm = data.Plant[3];
    std::string srchKornFamlNm = data.Plant[1];

    // API 요청 URL
    std::string url = "https://apis.data.go.kr/B554620/gardenPrntInfoService/getGardenPrntInfoList?serviceKey=";
    url += "cJIVRjkq4Qu4Ng4mt40n45%2BlfHDqGeLQFOFvPR2zmTBX8%2BP0oGcvsq%2Bsjlg0ljaBwE3UNoqpXlT1IQf2p%2BiqGQ%3D%3D&";
    url += "&type=json";
    url += "&srchScnm=" + urlEncode(srchScnm);   
    url += "&srchKrnm=" + urlEncode(srchKrnm);   
    url += "&srchFamlNm=" + urlEncode(srchFamlNm);            // 과명
    url += "&srchKornFamlNm=" + urlEncode(srchKornFamlNm);    // 한글과명                  
    

    curl_global_init(CURL_GLOBAL_DEFAULT);
    curl = curl_easy_init();

    if(curl)
    {
        curl_easy_setopt(curl, CURLOPT_URL, url.c_str());                       
        curl_easy_setopt(curl, CURLOPT_WRITEFUNCTION, Clnt_handler::WriteCallback);
        curl_easy_setopt(curl, CURLOPT_WRITEDATA, &readBuffer);

        res = curl_easy_perform(curl);

        if(res != CURLE_OK)
        {
            std::cout << (stderr, "curl_easy_perform() failed: %s\n", curl_easy_strerror(res));
        }
        else
        {
            // 역직렬화
            auto jsonResponse = json::parse(readBuffer);
            std::vector<PlantData> plants;

            for (auto& item : jsonResponse["response"]["body"]["items"]["item"])
            {
                PlantData plant;
                plant.CommonName = item.value("krnm", "");
                plant.KoreanFamilyName = item.value("kornFamlNm", "");
                plant.ScientificName = item.value("scnm", "");
                plant.FamilyName = item.value("famlNm", "");
                plant.FloweringPeriod = item.value("bloomPeriodCn", "");
                plant.FlowerColor = item.value("flwrClorCn","");
                plant.Info = item.value("fturCn","");
                plant.ImageUrl = item.value("imgUrl","");
                plants.push_back(plant);
            }

            if (plants.empty())
            {
                std::cout <<"검색결과가 없습니다.\n";

                sendData.Type = PLANT_SEARCH_NO_RESULT;	
                js = json{{"Type", sendData.Type}};
                serializedData = js.dump();
                send(mSock, serializedData.c_str(), serializedData.size(), 0);
                curl_easy_cleanup(curl);
                return;
            }
            else
            {
                int index = 0;
                // 구조체 데이터 출력 및 전송
                for (PlantData& plant : plants)
                {
                    std::cout << "Common Name: " << plant.CommonName << std::endl;
                    std::cout << "Korean Family Name: " << plant.KoreanFamilyName << std::endl;
                    std::cout << "Scientific Name: " << plant.ScientificName << std::endl;
                    std::cout << "Family Name: " << plant.FamilyName << std::endl;
                    std::cout << "Flowering Period: " << plant.FloweringPeriod << std::endl;
                    std::cout << "Info: " << plant.Info << std::endl;
                    std::cout << "ImageUrl: " << plant.ImageUrl << std::endl;
                    std::cout << "-----------------------------" << std::endl;
                    
                    sendData.Plant.resize(8);
                    sendData.Plant[0] = plant.CommonName;
                    sendData.Plant[1] = plant.KoreanFamilyName;
                    sendData.Plant[2] = plant.ScientificName;
                    sendData.Plant[3] = plant.FamilyName;
                    sendData.Plant[4] = plant.FloweringPeriod;
                    sendData.Plant[5] = plant.FlowerColor;
                    sendData.Plant[6] = plant.Info;
                    sendData.Plant[7] = plant.ImageUrl;

                    sendData.Type = PLANT_SEARCH;
                    js = json{{"Type", sendData.Type},{"Plant", sendData.Plant}};
                    serializedData = js.dump();
                    int bytesSent = send(mSock, serializedData.c_str(), serializedData.size(), 0);

                    usleep(100000);
                }

                sendData.Type = PLANT_SEARCH_END;
                js = json{{"Type", sendData.Type}};
                serializedData = js.dump();
                int bytesSent = send(mSock, serializedData.c_str(), serializedData.size(), 0);        
                if (bytesSent == -1)
                {
                    perror("send failed");
                }        
            }
        }
        curl_easy_cleanup(curl);
    }
    else
    {
        // 오류 처리
    }
}

void Clnt_handler::tryQuestion()
{
    CURL* curl;
    CURLcode res;
    std::string readBuffer;
    Data sendData;
    json js;
    std::string serializedData;

    srand((unsigned int)time(NULL));
    int num = rand();
    std::string a = std::to_string((int)num % 33 + 1);

    // API 요청 URL
    std::string url = "https://apis.data.go.kr/B554620/gardenPrntInfoService/getGardenPrntInfoList?serviceKey=";
    url += "cJIVRjkq4Qu4Ng4mt40n45%2BlfHDqGeLQFOFvPR2zmTBX8%2BP0oGcvsq%2Bsjlg0ljaBwE3UNoqpXlT1IQf2p%2BiqGQ%3D%3D&";
    url += "&pageNo=" + a;
    url += "&numOfRows=3";
    url += "&type=json";
    

    curl_global_init(CURL_GLOBAL_DEFAULT);
    curl = curl_easy_init();

    if(curl)
    {
        curl_easy_setopt(curl, CURLOPT_URL, url.c_str());                       
        curl_easy_setopt(curl, CURLOPT_WRITEFUNCTION, Clnt_handler::WriteCallback);
        curl_easy_setopt(curl, CURLOPT_WRITEDATA, &readBuffer);

        res = curl_easy_perform(curl);

        if(res != CURLE_OK)
        {
            std::cout << (stderr, "curl_easy_perform() failed: %s\n", curl_easy_strerror(res));
        }
        else
        {
            // 역직렬화
            auto jsonResponse = json::parse(readBuffer);
            std::vector<PlantData> plants;

            for (auto& item : jsonResponse["response"]["body"]["items"]["item"])
            {
                PlantData plant;
                plant.CommonName = item.value("krnm", "");
                plant.KoreanFamilyName = item.value("kornFamlNm", "");
                plant.ScientificName = item.value("scnm", "");
                plant.FamilyName = item.value("famlNm", "");
                plant.FloweringPeriod = item.value("bloomPeriodCn", "");
                plant.FlowerColor = item.value("flwrClorCn","");
                plant.Info = item.value("fturCn","");
                plant.ImageUrl = item.value("imgUrl","");
                plants.push_back(plant);
            }

            if (plants.empty())
            {
                std::cout <<"검색결과가 없습니다.\n";

                sendData.Type = PLANT_SEARCH_NO_RESULT;	
                js = json{{"Type", sendData.Type}};
                serializedData = js.dump();
                send(mSock, serializedData.c_str(), serializedData.size(), 0);
                curl_easy_cleanup(curl);
                return;
            }
            else
            {
                int index = 0;
                // 구조체 데이터 출력 및 전송
                for (PlantData& plant : plants)
                {
                    std::cout << "Common Name: " << plant.CommonName << std::endl;
                    std::cout << "Korean Family Name: " << plant.KoreanFamilyName << std::endl;
                    std::cout << "Scientific Name: " << plant.ScientificName << std::endl;
                    std::cout << "Family Name: " << plant.FamilyName << std::endl;
                    std::cout << "Flowering Period: " << plant.FloweringPeriod << std::endl;
                    std::cout << "Info: " << plant.Info << std::endl;
                    std::cout << "ImageUrl: " << plant.ImageUrl << std::endl;
                    std::cout << "-----------------------------" << std::endl;
                    if (index == 0)
                    {
                        sendData.Plant.resize(8);
                        sendData.Plant[0] = plant.CommonName;
                        sendData.Plant[1] = plant.KoreanFamilyName;
                        sendData.Plant[2] = plant.ScientificName;
                        sendData.Plant[3] = plant.FamilyName;
                        sendData.Plant[4] = plant.FloweringPeriod;
                        sendData.Plant[5] = plant.FlowerColor;
                        sendData.Plant[6] = plant.Info;
                        sendData.Plant[7] = plant.ImageUrl;
                        index++;
                    }
                    else
                    {
                        sendData.Plant.resize(1);
                        sendData.Plant[0] = plant.ImageUrl;
                    }

                    sendData.Type = QUESTION_INFO;
                    js = json{{"Type", sendData.Type},{"Plant", sendData.Plant}};
                    serializedData = js.dump();
                    int bytesSent = send(mSock, serializedData.c_str(), serializedData.size(), 0);
                    usleep(500000);
                }

                sendData.Type = QUESTION_END;
                js = json{{"Type", sendData.Type}};
                serializedData = js.dump();
                int bytesSent = send(mSock, serializedData.c_str(), serializedData.size(), 0);        
                if (bytesSent == -1)
                {
                    perror("send failed");
                }        
            }
        }
        curl_easy_cleanup(curl);
    }
    else
    {
        // 오류 처리
    }
}

size_t Clnt_handler::WriteCallback(void* contents, size_t size, size_t nmemb, void* userp)
{
    ((std::string*)userp)->append((char*)contents, size * nmemb);
    return size * nmemb;
}

std::string Clnt_handler::urlEncode(const std::string &value) {
    CURL *curl = curl_easy_init();
    if (curl) {
        char *output = curl_easy_escape(curl, value.c_str(), value.length());
        if (output) {
            std::string encoded(output);
            curl_free(output);
            curl_easy_cleanup(curl);
            return encoded;
        }
        curl_easy_cleanup(curl);
    }
    return "";
}

void Clnt_handler::tryChat(int nSock)
{
    Data sendData;
    json js;

    while (1)
    {
        char buffer[1024] = {0};

        // 데이터를 받는다
        int read = recv(mSock, buffer, 1024, 0);

        // 역직렬화
        std::string temp(buffer, read);
        json recvJson = json::parse(temp);

        Data recvData;
        recvJson.at("Type").get_to(recvData.Type);

        if (recvData.Type == CHAT_MESSAGE)
        {
            // 메세지면 전달
            recvJson.at("Chat").get_to(recvData.Chat);
            js = json{{"Type", recvData.Type},{"Chat", recvData.Chat}};
            std::string serializedData = js.dump();
            send(nSock, serializedData.c_str(), serializedData.size(), 0);
        }
        else if (recvData.Type == EMPLOYEE_CHAT_END_TRY)
        {
            break;
        }
    }
}

void Clnt_handler::tryChat2(int nSock)
{
    Data sendData;
    sendData.Type = CUSTOMER_CHAT_BEGIN_SUCCEED;
    json js;
    js = json{{"Type", sendData.Type}};
    std::string serializedData = js.dump();
    send(mSock, serializedData.c_str(), serializedData.size(), 0);


    sendData.Type = EMPLOYEE_CHAT_BEGIN_SUCCEED;
    js = json{{"Type", sendData.Type}};
    serializedData = js.dump();
    send(nSock, serializedData.c_str(), serializedData.size(), 0);


    while (1)
    {
        char buffer[1024] = {0};

        // 데이터를 받는다
        int read = recv(mSock, buffer, 1024, 0);

        // 역직렬화
        std::string temp(buffer, read);
        json recvJson = json::parse(temp);

        Data recvData;
        recvJson.at("Type").get_to(recvData.Type);

        if (recvData.Type == CHAT_MESSAGE)
        {
            // 메세지면 전달
            recvJson.at("Chat").get_to(recvData.Chat);
            js = json{{"Type", recvData.Type},{"Chat", recvData.Chat}};
            serializedData = js.dump();
            send(nSock, serializedData.c_str(), serializedData.size(), 0);
        }
        else if (recvData.Type == CUSTOMER_CHAT_END_TRY)
        {
            sendData.Type = CUSTOMER_CHAT_END_TRY_SUCCED;
            js = json{{"Type", sendData.Type}};
            std::string serializedData = js.dump();
            send(mSock, serializedData.c_str(), serializedData.size(), 0);

            sendData.Type = EMPLOYEE_CHAT_END_TRY;
            js = json{{"Type", sendData.Type}};
            serializedData = js.dump();
            send(nSock, serializedData.c_str(), serializedData.size(), 0);

            break;
        }
    }
}

void Clnt_handler::noService()
{
    Data sendData;
    sendData.Type = EMPLOYEE_CHAT_BEGIN_FAIL;
    json js;
    js = json{{"Type", sendData.Type}};
    std::string serializedData = js.dump();
    send(mSock, serializedData.c_str(), serializedData.size(), 0);
}

void Clnt_handler::showQNA(const Data & data, bool isEMP)
{
    int number;     // USER_NO
    std::string QTitle;
    std::string Content;
    int Check;
    Data sendData;
    json testset;
    std::vector<json> test_q;
    try
    {
        DB temp;
        temp.setAccount(mDB_Account, mDB_Password);
        sql::Connection* tempCoon = temp.connect();

        // USER 테이블에서 NO를 가져오기 위함
        sql::PreparedStatement *CheckNO =
            tempCoon->prepareStatement("SELECT NO FROM USER WHERE USER_ID = ?");
        CheckNO->setString(1, data.User[0]);
        sql::ResultSet *NORes = CheckNO->executeQuery();
        while (NORes->next())
        {
            number = NORes->getInt(1);
        }
        std::cout << "number: " << number << std::endl;         // 확인용

        // 질문 제목, 내용 가져오기 위함
        sql::PreparedStatement *CheckQ =
            tempCoon->prepareStatement("SELECT TITLE, QUESTION, CHECK_ANSWER FROM QnA WHERE QnA_UID = ?");
        CheckQ->setInt(1, number);
        sql::ResultSet *QRes = CheckQ->executeQuery();
        json js;
        while(QRes->next())
        {
            QTitle = QRes->getString(1);
            Content = QRes->getString(2);
            Check = QRes->getInt(3);
            js = json{
                {"QTitle", QTitle},
                {"Content", Content},
                {"Check", Check}};
            std::string serializedData = js.dump();
            send(mSock, serializedData.c_str(), serializedData.size(), 0);
            usleep(100000);
        }
        js = json{{"Type", QNA_FINISH}};
        std::string serializedData1 = js.dump();
        send(mSock, serializedData1.c_str(), serializedData1.size(), 0);

        // 데이터 확인
        std::cout << "QNA_SUCCED\n";
    }
    catch(const std::exception& e)
    {
        // db연결 중에 오류가 날 때 (조회 실패)
        std::cerr << "DB 오류 " << e.what() << '\n';
        sendData.Type = QNA_FAIL;

        json js = {{"Type", QNA_FAIL}};
        std::string serializedData = js.dump();
        send(mSock, serializedData.c_str(), serializedData.size(), 0);

        // 데이터 확인 
        std::cout << "QNA_FAIL\n";
    }
}

void Clnt_handler::showAnswer(const Data & data)
{
    std::string QTitle;
    std::string Content;
    Data sendData;
    json testset;
    try
    {
        DB temp;
        temp.setAccount(mDB_Account, mDB_Password);
        sql::Connection* tempCoon = temp.connect();

        // USER 테이블에서 NO를 가져오기 위함
        sql::PreparedStatement *CheckAnswer =
            tempCoon->prepareStatement("SELECT ANSWER FROM QnA WHERE TITLE = '너 누기야'");
        // CheckAnswer->setString(1, data.QTitle);
        sql::ResultSet *NORes = CheckAnswer->executeQuery();
        while (NORes->next())
        {
            Content = NORes->getString(1);
        }
        std::cout << "Answer: " << Content << std::endl;         // 확인용

        json js;
        js = json{{"Content", Content}};
        std::string serializedData1 = js.dump();
        send(mSock, serializedData1.c_str(), serializedData1.size(), 0);

        // 데이터 확인
        std::cout << "QNA_PAGE_NEXT_SUCCED\n"; 
    }
    catch(const std::exception& e)
    {
        // db연결 중에 오류가 날 때 (조회 실패)
        std::cerr << "DB 오류 " << e.what() << '\n';
        sendData.Type = QNA_FAIL;

        json js = {{"Type", QNA_FAIL}};
        std::string serializedData = js.dump();
        send(mSock, serializedData.c_str(), serializedData.size(), 0);

        // 데이터 확인 
        std::cout << "QNA_FAIL\n";
    }
}

void Clnt_handler::tryChart(const Data &data)
{
    Data sendData;
    UserData user;
    ResultData resultdata;
    json js = {
        {"Type", CHARTS_QUESTION_SUCCEED},
        {"Chart", json::array()}
    };
    int id;
    int corr;
    int incorr;
    // js["Type"] = CHARTS_QUESTION_SUCCEED;
try
    {
        DB temp;
        temp.setAccount(mDB_Account, mDB_Password);
        sql::Connection *tempCoon = temp.connect();

        sql::PreparedStatement *result =
            tempCoon->prepareStatement("SELECT RESULT_ID, CORRECT, INCORRECT FROM TEST_RESULT");
        sql::ResultSet *trRes = result->executeQuery();

        while (trRes->next())
        {
            id = trRes->getInt(1);
            corr = trRes->getInt(2);
            incorr = trRes->getInt(3);

            json chartEntry = {
                {"Id", id},
                {"Correct", corr},
                {"Incorrect", incorr}
            };

            js["Chart"].push_back(chartEntry);
        }

        // 데이터 직렬화 및 전송
        std::string serializedData = js.dump();
        send(mSock, serializedData.c_str(), serializedData.size(), 0);

        // 확인
        std::cout << "CHARTS_QUESTION_SUCCEED\n";
        std::cout << js.dump(4) << std::endl;

        // 차트 검색 완료 메시지 전송
        json endMessage = {{"Type", CHARTS_SEARCH_END}};
        std::string endData = endMessage.dump();
        send(mSock, endData.c_str(), endData.size(), 0);
    }
    catch (const std::exception &e)
    {
        std::cerr << "DB 오류 " << e.what() << '\n';

        // 데이터 확인
        std::cout << "차트 확인 실패";

        // 실패 메시지 전송
        json errorMessage = {{"Type", CHARTS_QUESTION_FAIL}, {"Message", e.what()}};
        std::string errorData = errorMessage.dump();
        send(mSock, errorData.c_str(), errorData.size(), 0);
    }
}