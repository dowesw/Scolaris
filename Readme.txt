Les incontournables
	- BLL
		Liaison base de donn�es application
	- DAO
		Traitement de la base de donn�es
	- ENTITE
		Representation r�elle des table de la base de donn�es
	- TOOLS
		Classe outil necessaire pour la centralisation des op�rations dans l'application
	- IMG
		Base de donn�es des images de l'application
		
Les dossiers BLL, DAO, ENTITE seront diviv�s en fonction des modules
	1- Donn�es de base [DB]
	2- Etablissement [EB] 
	3- Ressource Humaine [RH]
	4- Ann�e Acad�mique [AA]
	5- Biblioth�que [BT]
	6- Parascolaire [PS]
	7- Cit� Acad�mique [CA]
	
Contenus du dossier TOOLS
	- Connexion
		Classe de cr�ation de connexion
	- Constantes
		Classe des contantes... evitons d'utiliser des �l�ments fig�s dans le code
	- Utils
		Claase des fonctions globales
	- Chemins
		Classe des chemins de l'application... chemins vers les dossiers divers
	- Messages
		Classe des messages
	- Mots
		Dictionnaire de l'application... elle nous servira de traducteur local
	- Configuration
		Classe de configuration du visuel de l'interface
	- ReadWrite
		Classe des fonction de creation et de lecture des fichiers txt et csv
	- Logs
		Classe d'utilisation des fonction txt et csv
	- ObjectThread
		Classe de modification des objets par un thread