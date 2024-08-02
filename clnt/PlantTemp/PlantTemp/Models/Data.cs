using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantTemp.Models
{
    public class Data
    {
        public DataType Type { get; set; }
        public List<string>? User { get; set; }
        public List<string>? Plant { get; set; }
        public List<string>? QNA { get; set; }

        public List<ResultData> Chart { get; set; } = new List<ResultData>();

        public string? Chat { get; set; }

        public string? QTitle { get; set; }
        public string? Content { get; set; }
        public int Check { get; set; }
    }

    public class UserData
    {
        public string? ID { get; set; }     // ID (nullable)
        public string? PW { get; set; }     // PW (nullable)
        public string? Name { get; set; }   // 이름 (nullable)
        public string? Email { get; set; }  // 이메일 (nullable)
    }

    public enum User
    {
        ID,
        PW,
        NAME,
        Email,
    }



    public enum Plant
    {
        CommonName,           // 국명
        KoreanFamilyName,     // 한글과명
        ScientificName,       // 학명
        FamilyName,           // 과명
        FloweringPeriod,      // 개화기간
        FlowerColor,          // 꽃 색상
        Info,                 // 정보
        ImageUrl,             // 이미지
    };

    public class ResultData
    {
        public int Correct { get; set; }

        public int Id { get; set; }
        public int Incorrect { get; set; }
        public int? Type { get; set; }
    }

    public class QNAData
    {
        public string? Q_Title { get; set; }
        public string? Q_Text { get; set; }
        public string? Q_Check { get; set; }
    }




    public enum DataType
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
}