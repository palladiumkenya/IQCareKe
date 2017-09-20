using System;
using System.Runtime.Remoting;

using Application.BusinessProcess;
using System.Diagnostics;

using System.Threading;

namespace RemServer
{
    class StartPoint
    {
        [STAThread]
 
        static void Main(string[] args)
        {
            System.Diagnostics.Process theProc = Process.GetCurrentProcess();
            string s = theProc.MainModule.FileName;
            s = s + ".config";
            string Config = @"RemServer.exe.config";
            RemotingConfiguration.Configure(Config, false);
            RemotingConfiguration.ApplicationName = "IQCAREEMR";
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(BusinessServerFactory), "BusinessProcess.rem", WellKnownObjectMode.Singleton);
            Console.Write("Business Server Activated. Press Any Key to Disconnect");

            //   ListenerAsync.StartListening();
           Thread ilThread = new Thread(new ThreadStart(DataAccess.Interop.SocketListenerAsync.StartListening));
            //  DataAccess.Interop.SocketListenerAsync.StartListening();
           ilThread.Start();

          /*DataAccess.Interop.SocketClientAsync.SendToClient(@"{
  ""MESSAGE_HEADER"" : {""SENDING_APPLICATION"" : ""IQCARE"",
    ""SENDING_FACILITY"" : ""10829"", ""RECEIVING_APPLICATION"" : ""IL"",
    ""RECEIVING_FACILITY"" : ""10829"",  ""MESSAGE_DATETIME"" : ""20170919110000"",
    ""SECURITY"" : """", ""MESSAGE_TYPE"" : ""ADT^A04"", ""PROCESSING_ID"" : ""P""
  },
  ""PATIENT_IDENTIFICATION"" : {                   ""EXTERNAL_PATIENT_ID"" : {
                        ""ID"" : ""110ec58a-a0f2-4ac4-8393-c866d813b8d1"",
      ""IDENTIFIER_TYPE"" : ""GODS_NUMBER"",
      ""ASSIGNING_AUTHORITY"" : ""MPI""
                    },
    ""INTERNAL_PATIENT_ID"" : [
      {
        ""ID"" : ""88887777"",
        ""IDENTIFIER_TYPE"" : ""CCC_NUMBER"",
        ""ASSIGNING_AUTHORITY"" : ""CCC""
      },
      {
        ""ID"" : ""27326037"",
        ""IDENTIFIER_TYPE"" : ""NATIONAL_ID"",
        ""ASSIGNING_AUTHORITY"" : ""GOK""
      },
      {
        ""ID"" : ""098765ABC"",
        ""IDENTIFIER_TYPE"" : ""NHIF"",
        ""ASSIGNING_AUTHORITY"" : ""NHIF""
      }
    ],
    ""PATIENT_NAME"" : {
      ""FIRST_NAME"" : ""Cryprto Receiver"",
      ""MIDDLE_NAME"" : ""BowandArrow"",
      ""LAST_NAME"" : ""Club""
    },
    ""MOTHER_MAIDEN_NAME"" : ""Luanda Magere"",
    ""DATE_OF_BIRTH"" : ""19721223"",
    ""SEX"" : ""F"",
    ""PATIENT_ADDRESS"" : {
      ""PHYSICAL_ADDRESS"" : {
        ""VILLAGE"" : ""KwaMichael"",
        ""WARD"" : ""Sigiri"",
        ""SUB_COUNTY"" : ""Taita EAST"",
        ""COUNTY"" : ""Kwale""
      },
      ""POSTAL_ADDRESS"" : ""789 Kwale""
    },
    ""PHONE_NUMBER"" : ""254720278654"",
    ""MARITAL_STATUS"" : ""D"",
    ""DEATH_DATE"" : """",
    ""DEATH_INDICATOR"" : ""N""
  },
  ""NEXT_OF_KIN"" : [
    {
      ""NOK_NAME"" : {
        ""FIRST_NAME"" : ""Majani"",
        ""MIDDLE_NAME"" : ""KIMUTAI"",
        ""LAST_NAME"" : ""Suleimani""
      },
      ""RELATIONSHIP"" : ""**AS DEFINED IN GREENCARD"",
      ""ADDRESS"" : ""4678 KIAMBU"",
      ""PHONE_NUMBER"" : ""25489767899"",
      ""SEX"" : ""F"",
      ""DATE_OF_BIRTH"" : ""19871022"",
      ""CONTACT_ROLE"" : ""T""
    }
  ],
  ""OBSERVATION_RESULT"" : [
    {
      ""OBSERVATION_IDENTIFIER"" : ""START_HEIGHT"",
      ""CODING_SYSTEM"" : """",
      ""VALUE_TYPE"" : ""NM"",
      ""OBSERVATION_VALUE"" : ""120"",
      ""UNITS"" : ""CM"",
      ""OBSERVATION_RESULT_STATUS"" : ""F"",
      ""OBSERVATION_DATETIME"" : ""20170713110000"",
      ""ABNORMAL_FLAGS"" : ""N""
    },
    {
      ""OBSERVATION_IDENTIFIER"" : ""START_WEIGHT"",
      ""CODING_SYSTEM"" : """",
      ""VALUE_TYPE"" : ""NM"",
      ""OBSERVATION_VALUE"" : ""120"",
      ""UNITS"" : ""KG"",
      ""OBSERVATION_RESULT_STATUS"" : ""F"",
      ""OBSERVATION_DATETIME"" : ""20170713110000"",
      ""ABNORMAL_FLAGS"" : ""N""
    },
    {
      ""OBSERVATION_IDENTIFIER"" : ""IS_PREGNANT"",
      ""CODING_SYSTEM"" : """",
      ""VALUE_TYPE"" : ""CE"",
      ""OBSERVATION_VALUE"" : ""N"",
      ""UNITS"" : ""YES/NO"",
      ""OBSERVATION_RESULT_STATUS"" : ""F"",
      ""OBSERVATION_DATETIME"" : ""20170713110000"",
      ""ABNORMAL_FLAGS"" : ""N""
    },
    {
      ""OBSERVATION_IDENTIFIER"" : ""PRENGANT_EDD"",
      ""CODING_SYSTEM"" : """",
      ""VALUE_TYPE"" : ""D"",
      ""OBSERVATION_VALUE"" : ""20170713110000"",
      ""UNITS"" : ""DATE"",
      ""OBSERVATION_RESULT_STATUS"" : ""F"",
      ""OBSERVATION_DATETIME"" : ""20170713110000"",
      ""ABNORMAL_FLAGS"" : ""N""
    },
    {
      ""OBSERVATION_IDENTIFIER"" : ""CURRENT_REGIMEN"",
      ""CODING_SYSTEM"" : ""NASCOP_CODES"",
      ""VALUE_TYPE"" : ""CE"",
      ""OBSERVATION_VALUE"" : ""AF1A"",
      ""UNITS"" : """",
      ""OBSERVATION_RESULT_STATUS"" : ""F"",
      ""OBSERVATION_DATETIME"" : ""20170713110000"",
      ""ABNORMAL_FLAGS"" : ""N""
    },
    {
      ""OBSERVATION_IDENTIFIER"" : ""IS_SMOKER"",
      ""CODING_SYSTEM"" : """",
      ""VALUE_TYPE"" : ""CE"",
      ""OBSERVATION_VALUE"" : ""Y"",
      ""UNITS"" : ""YES/NO"",
      ""OBSERVATION_RESULT_STATUS"" : ""F"",
      ""OBSERVATION_DATETIME"" : ""20170713110000"",
      ""ABNORMAL_FLAGS"" : ""N""
    },
    {
      ""OBSERVATION_IDENTIFIER"" : ""IS_ALCOHOLIC"",
      ""CODING_SYSTEM"" : """",
      ""VALUE_TYPE"" : ""CE"",
      ""OBSERVATION_VALUE"" : ""Y"",
      ""UNITS"" : ""YES/NO"",
      ""OBSERVATION_RESULT_STATUS"" : ""F"",
      ""OBSERVATION_DATETIME"" : ""20170713110000"",
      ""ABNORMAL_FLAGS"" : ""N""
    }
  ]
}~");*/
            Console.ReadLine();
           
           
        }
    }
}

