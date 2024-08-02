#include "DB.h"

DB::DB() { }

DB::~DB() { }

void DB::setAccount(const std::string& id, const std::string& pw)
{
    mID = id;
    mPW = pw;
}

sql::Connection* DB::connect()
{
    try
    {
        sql::Driver* driver = sql::mariadb::get_driver_instance();
        sql::SQLString url = "jdbc:mariadb://10.10.21.110:3306/PLANT";    // db의 주소
        sql::Properties properties({{"user", mID}, {"password", mPW}});     // 로그인
        std::cout << "DB 접속 성공\n";
        return driver->connect(url, properties);
    }
    catch(sql::SQLException& e)
    {
        std::cerr << "DB 접속 실패: " << e.what() << std::endl;
        exit(1);
    }
}

void DB::disconnect(sql::Connection* conn)
{
    if (!conn->isClosed())
    {
        conn->close();
        std::cout << "DB 접속 해제\n";
    }
}