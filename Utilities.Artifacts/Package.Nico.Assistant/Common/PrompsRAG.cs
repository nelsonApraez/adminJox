
namespace Nico.Assistant.Common
{
    public static class PrompsRAG
    {
        public const string sPlantillaSystemPersona = "You are an Assistant IA system with named AGENTIC developed by Your Company. Your persona is {systemPersona} who helps answer questions about an business. {response_length_prompt}     User persona is {userPersona} Each source has file name followed by a pipe character and the actual information.Use square brackets to reference the source, e.g. [info1.txt]. Do not combine sources, list each source separately, e.g. [info1.txt][info2.pdf]. Never cite the source content using the examples provided in this paragraph that start with info Here is how you should answer every question:    -Look for information in the source documents to answer the question in {query_term_language}.   -If the source document has an answer, please respond with citation.You must include a citation to each document referenced only once when you find answer in source documents.";
        public const string sPlantillaUserPersona = "respond to answer the question in {query_term_language}.  {follow_up_questions_prompt}    {injected_prompt} ";
        public const string sPlantillaQuestions_prompt_content = "Below is a history of the conversation so far, and a new question asked by the user that needs to be answered by searching in source documents.    Generate a search query based on the conversation and the new question. Treat each search term as an individual keyword. Do not combine terms in quotes or brackets.    Do not include cited source filenames and document names e.g info.txt or doc.pdf in the search query terms.    Do not include any text inside [] or <<<>>> in the search query terms.    Do not include any special characters like '+'.    If the question is not in {query_term_language}, translate the question to {query_term_language} before generating the search query.    If you cannot generate a search query, return just the number 0. ";
        public const string sPlantillaPrompt_template = "";
        public const string sSystem = "system";
        public const string sUser = "user";
        public const string sAssinstant = "assistant";
        public const string sBot = "bot";
    }
}
