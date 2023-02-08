using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using msdyncrmCustomAPITools;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace msdyncrmWorkflowTools
{
    public class StringFunctions : IPlugin
    {
        #region return parameters 
        public string CapitalizedText;
        public string PaddedText;
        public string ReplacedText;
        public string SubstringText;
        public string TrimmedText;
        public string RegexText;
        public string UppercaseText;
        public string LowercaseText;
        public string WithoutSpaces;

        public int TextLength;

        public bool RegexSuccess;

        #endregion
        public void Execute(IServiceProvider serviceProvider)
        {
            // Obtain the tracing service
            ITracingService tracingService =
            (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            // Obtain the execution context from the service provider.  
            IPluginExecutionContext context = (IPluginExecutionContext)
                serviceProvider.GetService(typeof(IPluginExecutionContext));

            if (context.MessageName.Equals("StringFunctions") && context.Stage.Equals(30))
            {
                try
                {
                    #region Input Parameters
                    string Input_Text = (string)context.InputParameters["Input Text"];
                    string PadCharacter = (string)context.InputParameters["Padding: Pad Character"];
                    string ReplaceOldValue = (string)context.InputParameters["Replace: Old Value"];
                    string ReplaceNewValue = (string)context.InputParameters["Replace: New Value"];
                    string RegularExpression = (string)context.InputParameters["Regular Expression"];

                    bool Capitalize_All_Words = (bool)context.InputParameters["Capitalize All Words"];
                    bool PadontheLeft = (bool)context.InputParameters["Padding: Pad on the Left"];
                    bool CaseSensitive = (bool)context.InputParameters["Replace: Case Sensitive"];
                    bool FinalLength = (context.InputParameters["Substring: From Left to Right"] != null) ? (bool)context.InputParameters["Padding: Final Length"] : true;
                    bool FromLefttoRight = (context.InputParameters["Substring: From Left to Right"] != null) ? (bool)context.InputParameters["Substring: From Left to Right"] : true;

                    int FinalLengthwithPadding = (context.InputParameters["Padding: Final Length"] != null) ? (int)context.InputParameters["Padding: Final Length"] : 10;
                    int StartIndex = (context.InputParameters["Substring: Start Index"] != null) ? (int)context.InputParameters["Substring: Start Index"] : 0;
                    int SubStringLength = (context.InputParameters["Substring: Length"] != null) ? (int)context.InputParameters["Substring: Length"] : 3;
                    #endregion



                    StringFunctionsLogic(Input_Text, Capitalize_All_Words, PadCharacter, PadontheLeft, FinalLengthwithPadding, ReplaceOldValue, ReplaceNewValue, CaseSensitive, FromLefttoRight, StartIndex, SubStringLength, RegularExpression);




                    context.OutputParameters["Capitalized Text"] = CapitalizedText;
                    context.OutputParameters["Text Length"] = TextLength;
                    context.OutputParameters["Padded Text"] = PaddedText;
                    context.OutputParameters["Replaced Text"] = ReplacedText;
                    context.OutputParameters["Substring Text"] = SubstringText;
                    context.OutputParameters["Trimmed Text"] = TrimmedText;
                    context.OutputParameters["Regex Success"] = RegexSuccess;
                    context.OutputParameters["Regex Text"] = RegexText;

                    context.OutputParameters["Uppercase Text"] = UppercaseText;
                    context.OutputParameters["Lowercase Text"] = LowercaseText;
                    context.OutputParameters["Without Spaces"] = WithoutSpaces;
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                tracingService.Trace("StringFunctions plug-in is not associated with the expected message or is not registered for the main operation.");
            }
        }
        public void StringFunctionsLogic(string input_Text, bool capitalize_All_Words, string pad_Character, bool padonthe_Left, int final_Lengthwith_Padding, string replace_Old_Value, string replace_New_Value, bool case_Sensitive, bool from_Leftto_Right, int start_Index, int sub_String_Length, string regular_Expression)
        {
            CapitalizedText = "";
            PaddedText = "";

            ReplacedText = "";
            SubstringText = "";
            TrimmedText = "";
            RegexText = "";
            UppercaseText = "";
            LowercaseText = "";
            WithoutSpaces = "";

            TextLength = 0;

            RegexSuccess = true;
    }

        protected override void Execute(CodeActivityContext executionContext)
        {

            #region "Load CRM Service from context"

            Common objCommon = new Common(executionContext);
            objCommon.tracingService.Trace("Load CRM Service from context --- OK");
            #endregion
            msdyncrmWorkflowTools_Class commonClass = new msdyncrmWorkflowTools_Class(objCommon.service, objCommon.tracingService);
            bool test = commonClass.StringFunctions(capitalizeAllWords, inputText, padCharacter, padontheLeft, finalLengthwithPadding, caseSensitive,
                replaceOldValue, replaceNewValue, subStringLength, startIndex, fromLefttoRight, regularExpression,
                ref capitalizedText, ref paddedText, ref replacedText, ref subStringText, ref regexText,
                ref uppercaseText, ref lowercaseText, ref regexSuccess, ref withoutSpaces);

        }
    }
}