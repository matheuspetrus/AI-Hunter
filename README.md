# Inteligência Artificial
Trabalho Prático - N1

#### Apresentação ####

Grupo: Matheus Petrus

Um protótipo de game com o problema caça e caçador foi desenvolvido nesse trablho. Onde foi usado a Unity para como engine.

![Imagem1](/ArquivosReadme/Screenshot_11.png?raw=true)

O jogo consiste em um coelho que quer recuperar as suas vaquinhas, no centro da tela temos a representação gráfica e na direita algumas informações e botões de ações 


![Imagem1](/ArquivosReadme/Screenshot_12.png?raw=true)

Os comandos da direita são bem simples:

->Você pode botar a quantidade de vacas para o coelho procurar.
->Logo abaixo tem um indicado de qual estado está a máquina de estados do coelho.
->Temos um indicador de quantos turnos já se passaram.
->Um slider para você controlar a transparência do grid, onde acontece as checagens da IA.
->Um botão de passar os turnos automaticamente.
->E por final temos um botão que passa os turnos manualmente.

![Imagem1](/ArquivosReadme/Screenshot_14.png?raw=true)

A IA da Vaquinha é muito simples, ela usa um algoritmo Dijkstra para mapear ao seu redor e selecionar uma direção para onde ir. E ela fica nesse loop.
para fazê-la funcionar basta incorporar em um game object o scritp Hunting e Dijkistra e definir os parâmetros conforme a sua necessidade. Você também deve dar a regência dela para o gerenciador de turnos que deve estar na cena.

![Imagem1](/ArquivosReadme/IACoelho2.jpg?raw=true)

A IA do coelho é bem simples, pois ele inicia em um estado de busca e mapeamento usando o algoritmo Dijkstra, quando uma vaquinha entra no raio de alcance dele, ele passa para o estado de  variação de caminho para o seu alvo, e então usando o algoritmo A* ele define seu trajeto, após isso ele entra no estado de perseguição onde ele avança ate o seu alvo e quando chega perto o suficiente ele muda de estado, indo para o estado de atacar, e ao terminar o ataque ele retorna para o estado de busca.

![Imagem1](/ArquivosReadme/Screenshot_15.png?raw=true)

para fazê-l funcionar basta incorporar em um game object o scritp Hunting, Dijkistra e A* e definir os parâmetros conforme a sua necessidade. Você também deve dar a regência dela para o gerenciador de turnos que deve estar na cena.
