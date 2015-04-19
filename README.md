# wordfinder
Console application to find a word in a webservice when the only resource is query word by index

- WordFinder -> Core library with the algorithm to find the words
- WordFinder.Repository -> Library with repository interfaces to easily switch repository (word webservice)
- WordFinder.WebserviceRepository -> Concrete implementation of the webservice repository
- WordFinder.Application -> Console application. The application can be invoked with the word to find as a argument or it can be also informed in runtime

Tests
- WordFinder.Test -> Unit and integration tests for the algorithm and domain (word)
- WordFinder.ApplicationTest -> Unit and integration tests for the application layer (messaging and integration with core lib)
- WordFinder.WebserviceRepository.Test -> Unit and integration tests for the webservice repository
- WordFinder.ApplicationSpec -> BDD - Acceptance tests using Specflow framework. Tests the two required scenarios


Project dependencies:
- Moq
- SpecFLow
- Restsharp
- MMLib.Extensions

All dependencies are managed by nuget, you must restore before running application / tests.


