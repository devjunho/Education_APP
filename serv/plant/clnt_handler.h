#ifndef S_HANDLER_H
#define S_HANDLER_H
#include <iostream>
#include <cstdlib>
#include <ctime>
#include <string>
#include <unistd.h>
#include <curl/curl.h>
#include <arpa/inet.h>
#include <nlohmann/json.hpp>

#include "DB.h"
#include "data.h"

using json = nlohmann::json;

class Clnt_handler
{
public:
    Clnt_handler(int sock);
    ~Clnt_handler();
    void tryLogin(const Data & data, bool isEMP);
    void trySignUP(const Data & data, bool isEMP);
    void tryCheckID(const Data & data);
    void trySearch(const Data & data);
    void tryQuestion();
    void showQNA(const Data & data, bool isEMP);
    void showAnswer(const Data & data);
    void tryChart(const Data & data);
    void noService();
    void tryChat(int nSock);
    void tryChat2(int nSock);
private:
    int mSock;
    std::string mDB_Account;
    std::string mDB_Password;
    static size_t WriteCallback(void* contents, size_t size, size_t nmemb, void* userp);
    std::string urlEncode(const std::string &value);
};

#endif