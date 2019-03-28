namespace Focas
{
    public class ReturnCodes
    {
        public enum Code : short
        {
            FILE_WRITE_DENIED = -35,
            UNANTICIPATED_EXCEPTION = -34,
            NOT_IMPLEMENTED = -33,
            DISCONNECTED = -32,
            ETHERNET_PROTOCOL = -17,
            ETHERNET_SOCKET = -16,
            DLL = -15,
            UNASSIGNED_3 = -14,
            UNASSIGNED_2 = -13,
            UNASSIGNED_1 = -12,
            HSSB_BUS = -11,
            HSSB_SYSTEM_2 = -10,
            HSSB_COMM = -9,
            HANDLE_NUMBER = -8,
            CNC_LIBRARY_MISMATCH = -7,
            ABNORMAL_LIBRARY_STATE = -6,
            HSSB_SYSTEM = -5,
            HSSB_SHARED_RAM_PARITY = -4,
            HSSB_FANUC_DRIVERS_INSTALLATION = -3,
            RESET_STOP_REQUEST_PENDING = -2,
            BUSY = -1,
            NORMAL = 0,
            FUNCTION_NOT_AVAILABLE = 1,
            DATA_BLOCK_LENGTH = 2,
            DATA_NUMBER = 3,
            DATA_ATTRIBUTE = 4,
            DATA = 5,
            NO_OPTION = 6,
            WRITE_PROTECTION = 7,
            MEMORY_OVERFLOW = 8,
            CNC_PARAMETER = 9,
            BUFFER_EMPTY_OR_FULL = 10,
            PATH_NUMBER = 11,
            CNC_MODE = 12,
            CNC_EXECUTION_REJECTION = 13,
            DATA_SERVER = 14,
            ALARM = 15,
            FILE_DOES_NOT_EXIST = 20,
            FUNCTION_FOR_30_SERIES_ONLY = 0X13,
            INVALID_PARAMETER_VALUE = 0X15,
            PASSWORD = 0X11,
            RETRY_COUNT_EXCEEDED = 0X12,
            STOP = 0X10
        }
    }
}
