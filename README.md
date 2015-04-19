# wordfinder
Console application to find a word in a webservice when the only resource is query word by index

- WordFinder -> Core library with the algorithm to find the words
- WordFinder.Repository -> Library with repository interfaces to easily switch repository (word webservice)
- WordFinder.WebserviceRepository -> Concrete implementation of the webservice repository
- WordFinder.Application -> Console application. The application can be invoked with the word to find as a argument or it can be also informed at runtime

Tests
- WordFinder.Test -> Unit and integration tests for the algorithm and domain (word)
- WordFinder.ApplicationTest -> Unit and integration tests for the application layer (messaging and integration with core lib)
- WordFinder.WebserviceRepository.Test -> Unit and integration tests for the webservice repository
- WordFinder.ApplicationSpec -> BDD - Acceptance tests using Specflow framework. Tests the two required result scenarios
(Seu programa deve receber uma palavra como parâmetro de entrada e retornar a posição da palavra informada, alguma mensagem caso a palavra não exista e o número de gatinhos mortos)

Project dependencies:
- Moq
- SpecFlow
- RestSharp
- MMLib.Extensions

All dependencies are managed by nuget, you must restore before running application / tests.


