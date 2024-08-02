#ifndef DATA_H
#define DATA_H
#include <string>
#include <vector>

struct Data
{
    int Type;
    std::vector<std::string> User;
    std::vector<std::string> Plant;
    std::vector<std::string> QNA;
    std::string Chat;
    std::string QTitle;
    std::string Content;
};

struct UserData
{
    std::string ID;     // ID
    std::string PW;     // PW
    std::string Name;   // 이름
    std::string Email;  // 이메일
};

struct ResultData
{
    int Type;
    int Id;
    int Correct;
    int Incorrect;
};

struct PlantData
{
    std::string CommonName;           // 국명
    std::string KoreanFamilyName;     // 한글과명
    std::string ScientificName;       // 학명
    std::string FamilyName;           // 과명
    std::string FloweringPeriod;      // 개화기간
    std::string FlowerColor;          // 꽃 색상
    std::string Info;                 // 정보
    std::string ImageUrl;	          // 이미지
};

struct QNAData
{
    std::string QNA;
    std::string Title;  // 제목
    std::string Value;  // 내용
};

enum DataType
{
    // 0번대 오류
    CONNECT_FAIL = 0,

    // 회원가입
    EMPLOYEE_SIGN_UP_TRY,
    CUSTOMER_SIGN_UP_TRY,

    SIGN_UP_SUCCEED,
    SIGN_UP_FAIL,

    SIGN_UP_CHECK_ID,
    SIGN_UP_CHECK_ID_SUCCED,
    SIGN_UP_CHECK_ID_FAIL,

    // 로그인
    EMPLOYEE_LOGIN_TRY,
    CUSTOMER_LOGIN_TRY,

    LOGIN_SUCCEED,
    LOGIN_FAIL,
    LOGIN_FAIL_ID,
    LOGIN_FAIL_PW,

    // 직원 챗
    EMPLOYEE_CHAT_BEGIN_TRY,
    EMPLOYEE_CHAT_BEGIN_SUCCEED,
    EMPLOYEE_CHAT_BEGIN_FAIL,
    EMPLOYEE_CHAT_END_TRY,
    EMPLOYEE_CHAT_END_TRY_SUCCED,
    EMPLOYEE_CHAT_END_TRY_FAIL,

    // 고객 챗
    CUSTOMER_AUTO_CHAT_TRY,
    CUSTOMER_CHAT_BEGIN_TRY,
    CUSTOMER_CHAT_BEGIN_SUCCEED,
    CUSTOMER_CHAT_BEGIN_FAIL,
    CUSTOMER_CHAT_BEGIN_WAIT,
    CUSTOMER_CHAT_END_TRY,
    CUSTOMER_CHAT_END_TRY_SUCCED,
    CUSTOMER_CHAT_END_TRY_FAIL,

    // 채팅
    CHAT_MESSAGE,
    CHAT_AUTO_MESSAGE,

    // 식물 조회
    PLANT_SEARCH_TRY,
    PLANT_SEARCH,
    PLANT_SEARCH_END,
    PLANT_SEARCH_NO_RESULT,

    // 문제 풀이
    QUESTION_TRY,
    QUESTION_INFO,
    QUESTION_END,

    // Q&A 
    QNA_CUSTOMER_TRY,
    QNA_SUCCED,
    QNA_FAIL,                   // 추가함
    QNA_FINISH,
    QNA_PAGE_NEXT_TRY,
    QNA_PAGE_NEXT_SUCCED,
    QNA_PAGE_PREV_TRY,
    QNA_PAGE_PREV_SUCCED,

    // 차트 
    CHARTS_PLANT_TRY,
    CHARTS_PLANT_SUCCEED,
    CHARTS_QUESTION_TRY,
    CHARTS_QUESTION_SUCCEED,
    CHARTS_QUESTION_FAIL,
    CHARTS_SEARCH_END,
    
};

#endif