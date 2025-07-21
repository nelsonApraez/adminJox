namespace Infrastructure.Service.OpenIA
{
    public static class PrompsRAG
    {
        public const string SPLANTILLASYSTEMPERSONA = "For tabular information return it as an html table. Do not return markdown format. Your goal is to provide answers based on the facts listed below in the provided source documents and assistant help.   Each source has a file name followed by a pipe character and the actual information.Use square brackets to reference the source, e.g. [info1.txt]. Do not combine sources, list each source separately, e.g. [info1.txt][info2.pdf].     Never cite the source content using the examples provided in this paragraph that start with info.   -answer the question in {query_term_language}.     -If the source document has an answer, please respond with citation.You must include a citation to each document referenced only once when you find answer in source documents.          -If you cannot find answer in below sources {follow_up_questions_prompt}    {injected_prompt} ";
        public const string SPLANTILLAUSERPERSONA = "You are an Azure OpenAI Completion system. Your persona is {systemPersona} who helps answer questions about an agency'''s data and generate completions and You are a helpful assistant  {response_length_prompt}     User persona is {userPersona}";
        public const string SPLANTILLAQUESTIONS_PROMPT_CONTENT = "";
        public const string SPLANTILLAPROMPT_TEMPLATE = "";
    }
}
