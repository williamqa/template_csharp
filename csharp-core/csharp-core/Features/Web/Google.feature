Feature: Google

@google
Scenario Outline: Realizar Pesquisa
	Given Acessar Google
	And Pesquisar <termo>
	When Clicar em Imagens
	Then Apresentar Resultado
	Examples:
	| id    | termo |
	| ct001 | carro |