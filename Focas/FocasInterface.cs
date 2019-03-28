using System;
using System.Runtime.InteropServices;
using static Focas.ReturnCodes;

namespace Focas
{
    public class FocasInterface
    {
        private string ipAddress;
        private ushort portNumber;
        private short timeout;
        private ushort focasHandle;
        private bool connected;

        public FocasInterface(string IPAddress, ushort PortNumber, short Timeout)
        {
            ipAddress = IPAddress;
            portNumber = PortNumber;
            timeout = Timeout;
        }

        public ReturnCodes.Code Connect()
        {
            try
            {
                var connectReturnCode = (ReturnCodes.Code)Focas1.cnc_allclibhndl3(ipAddress, portNumber, timeout, out focasHandle);

                if (connectReturnCode == ReturnCodes.Code.NORMAL)
                {
                    connected = true;
                    return connectReturnCode;
                }
                else
                {
                    connected = false;

                    Focas1.ODBERR error = new Focas1.ODBERR();
                    return (ReturnCodes.Code)Focas1.cnc_getdtailerr(focasHandle, error);
                }
            }
            catch (Exception ex)
            {
                connected = false;
                return ReturnCodes.Code.UNANTICIPATED_EXCEPTION;
            }
        }

        public void Disconnect()
        {
            if (!connected) return;

            try
            {
                Focas1.cnc_freelibhndl(focasHandle);
                connected = false;
            }
            catch (Exception ex)
            {
            }
        }

        public ReturnCodes.Code PathToGet(short Path)
        {
            try
            {
                return (ReturnCodes.Code)Focas1.cnc_setpath(focasHandle, Path);
            }
            catch (Exception ex)
            {
                return ReturnCodes.Code.UNANTICIPATED_EXCEPTION;
            }
        }

        public Status GetStatusInfo()
        {
            Focas1.ODBST odbst = new Focas1.ODBST();
            Status statusInfo = new Status();

            var statusReturnCode = (ReturnCodes.Code)Focas1.cnc_statinfo(focasHandle, odbst);

            statusInfo.returnCode = statusReturnCode;
            statusInfo.Auto = odbst.aut;
            statusInfo.Emergency = odbst.emergency;
            statusInfo.Run = odbst.run;
            statusInfo.Motion = odbst.motion;
            statusInfo.Edit = odbst.edit;

            return statusInfo;
        }

        public MacroVariable GetMacroVariable(short variableNumber)
        {
            Focas1.ODBM odbm = new Focas1.ODBM();
            MacroVariable macroVariable = new MacroVariable();

            var readMacroVariableReturnCode = (ReturnCodes.Code)Focas1.cnc_rdmacro(focasHandle, variableNumber, 10, odbm);

            macroVariable.returnCode = readMacroVariableReturnCode;
            macroVariable.VariableNumber = variableNumber;
            macroVariable.VariableValue = ((double)odbm.mcr_val) / Math.Pow(10.0, (double)odbm.dec_val);

            return macroVariable;
        }

        public Program GetProgramNumber()
        {
            Focas1.ODBPRO odbpro = new Focas1.ODBPRO();
            Program program = new Program();

            ReturnCodes.Code progamReturnCode = (ReturnCodes.Code) Focas1.cnc_rdprgnum(focasHandle, odbpro);

            program.returnCode = progamReturnCode;
            program.Main = odbpro.mdata;
            program.Running = odbpro.data;

            return program;
        }

        public Spindle GetSpindleOverride()
        {
            Focas1.IODBPMC0 iodbpmc0 = new Focas1.IODBPMC0();
            Spindle spindle = new Spindle();

            var spindleReturnCode = (ReturnCodes.Code) Focas1.pmc_rdpmcrng(focasHandle, (short)0, (short)0, (ushort)30, (ushort)30, (ushort)16, iodbpmc0);

            spindle.returnCode = spindleReturnCode;
            spindle.Override = (int)iodbpmc0.cdata[0];

            return spindle;
        }

        public AxisOverride GetAxisOverride()
        {
            Focas1.IODBPMC0 iodbpmc0 = new Focas1.IODBPMC0();
            AxisOverride axisOverride = new AxisOverride();

            var axisOverrideReturnCode = (ReturnCodes.Code)Focas1.pmc_rdpmcrng(focasHandle, (short)0, (short)0, (ushort)10, (ushort)12, (ushort)24, iodbpmc0);

            axisOverride.returnCode = axisOverrideReturnCode; 
            axisOverride.Override = ((int)byte.MaxValue - (int)iodbpmc0.cdata[2]);

            return axisOverride;
        }
    }
}