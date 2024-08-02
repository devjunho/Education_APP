#include <iostream>
#include <string>
#include <vector>
#include <unordered_map>
#include <queue>
#include <thread>
#include <condition_variable>
#include <unistd.h>
#include <arpa/inet.h>
#include <nlohmann/json.hpp>
#include "DB.h"
#include "data.h"
#include "clnt_handler.h"

using json = nlohmann::json;


#define PORT_NUM 9998

struct ClientInfo                   // 클라이언트 구조체
{                 
	int clnt_sock;                  // 클라이언트 소켓
	sockaddr_in clnt_adr;           // 클라이언트 소켓 주소
};

std::queue<int> customerQueue;       // 고객 대기 큐
std::queue<int> serviceQueue;        // 상담사 대기 큐  
std::vector<ClientInfo*> clnt_socks; // 클라이언트 소켓 관리 벡터
pthread_mutex_t mutx;                // 뮤텍스 선언
pthread_cond_t cond_customer;        // 고객 조건 변수
pthread_cond_t cond_service;         // 상담사 조건 변수

void error_handling(const char * msg);    // 에러 관리
void * handle_clnt(void * arg);

int main()
{
    int serv_sock;
    struct sockaddr_in serv_adr;

    pthread_mutex_init(&mutx, nullptr);             // 뮤텍스 준비

    serv_sock = socket(PF_INET, SOCK_STREAM, 0);

    if (serv_sock == -1)
    // 소켓 함수의 반환값이 -1이면 에러 발생 상황
    {
        error_handling("서버 소켓 생성 실패");
    }

    memset(&serv_adr, 0, sizeof(serv_adr));

    serv_adr.sin_family = AF_INET;
    serv_adr.sin_addr.s_addr = INADDR_ANY;
    serv_adr.sin_port = htons(PORT_NUM);

    if (bind(serv_sock, (struct sockaddr *)&serv_adr, sizeof(serv_adr)) < 0)
    {
        error_handling("서버 소켓 바인딩 실패");
    }

    if (listen(serv_sock, 5) == -1)
    {
        error_handling("서버 소켓 리슨 실패");
    }
    
    /** accept 시작 **/

    while (1)
    {
        int clnt_sock;
        struct sockaddr_in clnt_adr;
        socklen_t clnt_adr_sz = sizeof(clnt_adr_sz);

        clnt_sock = accept(serv_sock, (struct sockaddr*)&clnt_adr, &clnt_adr_sz);

        if (clnt_sock == -1) 
        {
            std::cerr << "클라이언트 연결 수락 실패";
            close(clnt_sock);
        }

        pthread_mutex_lock(&mutx);

        ClientInfo* clientData = new ClientInfo;
		clientData->clnt_sock = clnt_sock;
		clientData->clnt_adr = clnt_adr;
        clnt_socks.push_back(clientData);

        pthread_mutex_unlock(&mutx);
 
        pthread_t clnt_thread;
        pthread_create(&clnt_thread, nullptr, handle_clnt, (void*)clientData);

        pthread_detach(clnt_thread);
    }

    close(serv_sock);

    return 0;
}

void * handle_clnt(void * arg)
{
    ClientInfo* clientData = static_cast<ClientInfo*>(arg);
    int clnt_sock = clientData->clnt_sock;
	sockaddr_in clnt_adr = clientData->clnt_adr;
    char buffer[1024] = {0};

    try
    {
        // 데이터를 받는다
        int read = recv(clnt_sock, buffer, 1024, 0);

        // 역직렬화
        std::string temp(buffer, read);
        json recvJson = json::parse(temp);
        Data recvData;
        recvJson.at("Type").get_to(recvData.Type);

        // 데이터 확인 ( 타입 )
        std::cout << "sock: " << clnt_sock << " Type: " << recvData.Type << "\n";

        // 핸들러 객체 생성
        Clnt_handler clnt(clnt_sock);

        switch (recvData.Type)
        {
        case EMPLOYEE_SIGN_UP_TRY:
            recvJson.at("User").get_to(recvData.User);

            // 데이터 확인
            std::cout << "ID: " << recvData.User[0] << "\n";
            std::cout << "PW: " << recvData.User[1] << "\n";
            std::cout << "Name: " << recvData.User[2] << "\n";
            std::cout << "E-mail: " << recvData.User[3] << "\n";

            clnt.trySignUP(recvData, true);
            break;

        case CUSTOMER_SIGN_UP_TRY:
            recvJson.at("User").get_to(recvData.User);

            // 데이터 확인
            std::cout << "ID: " << recvData.User[0] << "\n";
            std::cout << "PW: " << recvData.User[1] << "\n";
            std::cout << "Name: " << recvData.User[2] << "\n";
            std::cout << "E-mail: " << recvData.User[3] << "\n";

            clnt.trySignUP(recvData, false);
            break;

        case SIGN_UP_CHECK_ID:
            recvJson.at("User").get_to(recvData.User);

            // 데이터 확인
            std::cout << "ID: " << recvData.User[0] << "\n";

            clnt.tryCheckID(recvData);
            break;

        case EMPLOYEE_LOGIN_TRY:
            recvJson.at("User").get_to(recvData.User);

            // 데이터 확인
            std::cout << "ID: " << recvData.User[0] << "\n";
            std::cout << "PW: " << recvData.User[1] << "\n";

            clnt.tryLogin(recvData, true);
            break;

        case CUSTOMER_LOGIN_TRY:
            recvJson.at("User").get_to(recvData.User);

            // 데이터 확인
            std::cout << "ID: " << recvData.User[0] << "\n";
            std::cout << "PW: " << recvData.User[1] << "\n";

            clnt.tryLogin(recvData, false);
            break;

        case PLANT_SEARCH_TRY:
            recvJson.at("Plant").get_to(recvData.Plant);

            // 데이터 확인
            std::cout << "조회조건\n";

            std::cout << "국명: " << recvData.Plant[0] << "\n";
            std::cout << "한글과명: " << recvData.Plant[1] << "\n";
            std::cout << "학명: " << recvData.Plant[2] << "\n";
            std::cout << "과명: " << recvData.Plant[3] << "\n";

            clnt.trySearch(recvData);
            break;
        
        case QUESTION_TRY:
            std::cout << "퀴즈 요청\n";
            clnt.tryQuestion();
            break;

        case EMPLOYEE_CHAT_BEGIN_TRY:

            // 데이터 확인 ( 완성 후 제거)
            std::cout << clnt_sock << "번호가 서비스 채팅 시도" << "\n";

            int customer_sock;
            pthread_mutex_lock(&mutx);

            // 상담사 대기 큐에 추가
            serviceQueue.push(clnt_sock);

            while (1)
            {
                pthread_cond_wait(&cond_service, &mutx);
                // 사용자가 noti할때까지 대기
                // 사용자가 noti하면 내차례인지 확인
                if (serviceQueue.front() == clnt_sock)
                {
                    std::cout << "내 차례\n";
                    break;
                }
            }

            customer_sock = customerQueue.front();
            std::cout << "상담사 " << clnt_sock << "와 고객 " << customer_sock << "가 연결\n";
            pthread_cond_broadcast(&cond_customer);

            pthread_mutex_unlock(&mutx);

            clnt.tryChat(customer_sock);

            break;

        case CUSTOMER_CHAT_BEGIN_TRY:

            // 데이터 확인 ( 완성 후 제거)
            std::cout << clnt_sock << "번호가 커스터머 채팅 시도" << "\n";

            int service_sock;
            pthread_mutex_lock(&mutx);
            
            if (serviceQueue.empty())
            {
                // 클라이언트에 상담사가 없음을 알림
                std::cout << "상담사가 없습니다.\n";
                pthread_mutex_unlock(&mutx);
                clnt.noService();
            }
            else
            {
                // 대기중인 서비스에게 알림
                pthread_cond_broadcast(&cond_service);

                // 고객 대기 큐에 추가
                customerQueue.push(clnt_sock);

                while (1)
                {
                    pthread_cond_wait(&cond_customer, &mutx);
                    // 상담사가 알릴 때까지 대기
                    // 상담사가 알리면 내 차례인지 확인
                    if (customerQueue.front() == clnt_sock)
                    {
                        break;
                    }
                }

                service_sock = serviceQueue.front();
                customerQueue.pop();
                serviceQueue.pop();
                std::cout << "상담사 " << service_sock << "와 고객 " << clnt_sock << "가 연결\n";
                pthread_mutex_unlock(&mutx);
                clnt.tryChat2(service_sock);
            }
            
            break;

            case QNA_PAGE_NEXT_TRY:
            recvJson.at("User").get_to(recvData.User);

            std::cout << "ID: " << recvData.User[0] << "\n";

            clnt.showAnswer(recvData);
            break;

            case QNA_CUSTOMER_TRY:
            recvJson.at("User").get_to(recvData.User);

            std::cout << "ID: " << recvData.User[0] << "\n";
            std::cout << "PW: " << recvData.User[1] << "\n";

            clnt.showQNA(recvData, false);
            break;
            
            case CHARTS_QUESTION_TRY:
            // recvJson.at("Chart").get_to(recvData.Chart);
            std::cout << "1\n";

            clnt.tryChart(recvData);
            break;
        default:
            break;
        }
    }
    catch(const std::exception& e)
    // 데이터 받음에 문제 발생
    {
        // 문제 확인
        std::cerr << e.what() << '\n';
        try
        // clnt에 전송 시도
        {
            Data sendData;
            sendData.Type = CONNECT_FAIL;
            json js;
            js = json{{"Type", sendData.Type}};
            std::string serializedData = js.dump();
            send(clnt_sock, serializedData.c_str(), serializedData.size(), 0);
        }
        catch(const std::exception& e)
        {
            std::cerr << e.what() << '\n';
        }
    }
    

    pthread_mutex_lock(&mutx);                    
    for (int i = 0; i < clnt_socks.size(); i++)
    {
		if (clnt_socks[i]->clnt_sock == clnt_sock)
        {
            while (i++ < clnt_socks.size() - 1)
            {
                clnt_socks[i] = clnt_socks[i + 1];
            }
            break;
		}
	}
    pthread_mutex_unlock(&mutx);

    std::cout << clnt_sock << "접속종료\n";
    delete clientData;
    close(clnt_sock);
    return nullptr;
}

void error_handling(const char *message)
{
    fputs(message, stderr);
    fputc('\n', stderr);
    exit(1);
}

