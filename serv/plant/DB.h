#ifndef DB_H
#define DB_H
#include <string>
#include <iostream>
#include <mariadb/conncpp.hpp>

class DB
{
public:
    DB();
    ~DB();
    void setAccount(const std::string& id, const std::string& pw);
    // DB 아이디 비밀번호 설정
    sql::Connection* connect();
    // db와 연결후 연결객체를 반환
    void disconnect(sql::Connection* conn);
    // 연결객체를 인자로 받아 연결해제
private:
    std::string mID;
    // DB 접속 아이디
    std::string mPW;
    // DB 접속 패스워드
};

#endif