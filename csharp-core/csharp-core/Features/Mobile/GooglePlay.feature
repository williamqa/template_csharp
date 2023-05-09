Feature: GooglePlay

@mytag
Scenario Outline: Procurar Aplicativo no Google Play
	Given Eu estou na Tela Principal
	And Eu clico em pesquisar
	And Eu informo um termo de busca <termo>
	When Eu clico no resultado
	Then Sou direcionado para a tela do Aplicativo
Examples:
| id    | termo        |
| ct001 | clash royale |