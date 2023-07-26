using System.ComponentModel;

namespace Graphql_example_code.Application.Core.Results;
public enum ErrorCodes 
{
    [Description("Title Can not Empty")]
    TITLE_CAN_NOT_EMPTY = 1,
    [Description("Title Maximum Length is 500")]
    TITLE_MAXIMUM_LENGTH_IS_500 = 2,
    [Description("Description Maximum Length is 1000")]
    DESCRIPTION_MAXIMUM_LENGTH_IS_1000 = 3,

}
