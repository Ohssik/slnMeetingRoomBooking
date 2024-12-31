using Dapper;
using MeetingRoomBooking.Common.Infrastructure.OutputWrapper.Exceptions;
using MeetingRoomBooking.Common.Infrastructure.OutputWrapper.Models;
using MeetingRoomBooking.Common.Models;
using System.Reflection;
using System.Text;

namespace MeetingRoomBooking.Repository.Models
{
    /// <summary>
    ///   產生SQL Query String的class
    /// </summary>
    public class SearchQueryCmdGenerator
    {
        StringBuilder _mainQueryCmd;
        BookingInputParamater _booking;
        DynamicParameters _parameters;

        /// <summary>
        ///   SQL Query String初始設定
        /// </summary>
        public SearchQueryCmdGenerator(BookingInputParamater booking)
        {
            _mainQueryCmd = new StringBuilder();
            _mainQueryCmd.Append("SELECT * FROM ViewTMeetingBooking WHERE 1 = 1");
            _booking = booking;
            _parameters = new DynamicParameters();
        }                

        /// <summary>
        ///   取得SQL Query String和SQL Parameters
        /// </summary>
        public (string,DynamicParameters) SearchParameters
        {
            get{ return (_mainQueryCmd.ToString(), _parameters); }
            set { }
        }

        /// <summary>
        ///   從判斷BookingInputParamater的屬性是否為空再去加上Query String和Parameters
        /// </summary>
        public void AddSqlCmd()
        {
            if (!string.IsNullOrEmpty(_booking.RoomId))
            {
                _mainQueryCmd.Append($" AND RoomId = @RoomId");
                _parameters.Add("RoomId", _booking.RoomId);
            }

            if (!string.IsNullOrEmpty(_booking.BookingUserId))
            {
                _mainQueryCmd.Append($" AND BookingUserId LIKE '%'+@BookingUserId+'%'");
                _parameters.Add("BookingUserId", _booking.BookingUserId);
            }

            if (_booking.Id.HasValue)
            {
                _mainQueryCmd.Append($" AND Id = @Id");
                _parameters.Add("Id", _booking.Id);
            }

            if (!string.IsNullOrEmpty(_booking.TargetDate))
            {
                _mainQueryCmd.Append($" AND StartDate = @TargetDate");
                _parameters.Add("TargetDate", _booking.TargetDate);
            }            
            
        }

        /// <summary>
        ///   從判斷BookingInputParamater的屬性SortBy,offset,PageSize是否為空再去加上Query String
        /// </summary>
        public void AddSortBySqlCmd()
        {
            Type type = typeof(BookingViewModel);
            PropertyInfo[] properties = type.GetProperties();

            string[] sortBySql = _booking.SortBy.ToLower().Split('_');

            if (sortBySql[0] == "targetdate")
            {
                sortBySql[0] = "StartDate";
            }

            if (properties.Any(p => p.Name.ToLower().Equals(sortBySql[0])))
            {
                _mainQueryCmd.Append($" ORDER BY {sortBySql[0]} ");
            }
            else if(string.IsNullOrEmpty(sortBySql[0]))
            {
                _mainQueryCmd.Append(" ORDER BY Id ");
            }
            else
            {
                // BadRequest Exception
                throw new ValidateException(new RequestValidateResult()
                {
                    ParameterName = "SortBy",
                    ParameterValue = _booking.SortBy,
                    Error = new ErrorMessage()
                    {
                        Code = "400",
                        Message = "Invalid sortBy parameter",
                        Description = "Invalid sortBy parameter"
                    }
                });

            }

            if (sortBySql.Length > 1)
            {
                if (sortBySql[1] == "asc" || sortBySql[1] == "desc")
                {
                    _mainQueryCmd.Append(sortBySql[1]);                    
                }
                else
                {
                    // BadRequest Exception                    
                    throw new ValidateException(new RequestValidateResult()
                    {
                        ParameterName = "SortBy",
                        ParameterValue = _booking.SortBy,
                        Error = new ErrorMessage()
                        {
                            Code = "400",
                            Message = "Invalid sortBy parameter",
                            Description = "Invalid sortBy parameter",
                            
                        }
                    });
                }
            }

            _mainQueryCmd.Append(" OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY");
            _parameters.Add("Offset", (_booking.PageIndex - 1) * _booking.PageSize);
            _parameters.Add("PageSize", _booking.PageSize);
            
        }

        // <summary>
        // 取得CountQuery
        // </summary>
        public string GetRecordsCountsSql
        {
            get
            {
                string sqlCmd = $"SELECT COUNT(*) FROM ({_mainQueryCmd.ToString()}) AS CountTable";
                return sqlCmd;
            }
            set { }
        }
    }
}
