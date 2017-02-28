Les incontournables
	- BLL
		Liaison base de données application
	- DAO
		Traitement de la base de données
	- ENTITE
		Representation réelle des table de la base de données
	- TOOLS
		Classe outil necessaire pour la centralisation des opérations dans l'application
	- IMG
		Base de données des images de l'application
		
Les dossiers BLL, DAO, ENTITE seront divivés en fonction des modules
	1- Données de base [DB]
	2- Etablissement [EB] 
	3- Ressource Humaine [RH]
	4- Année Académique [AA]
	5- Bibliothèque [BT]
	6- Parascolaire [PS]
	7- Cité Académique [CA]
	
Contenus du dossier TOOLS
	- Connexion
		Classe de création de connexion
	- Constantes
		Classe des contantes... evitons d'utiliser des élèments figés dans le code
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