using System; // Importation de la bibliothèque System 
using System.Text.Json; // Importation de la bibliothèque System.Text.Json (pour la sérialisation JSON)
using System.Text.RegularExpressions; // Importation de la bibliothèque System.Text.RegularExpressions (pour les expressions régulières ou regex)

namespace projetbibli
{ // Déclaration du namespace projetbibli
    class Livre { // Déclaration de la classe Livre
        public string titre { get; set; } // Déclaration de la propriété titre de type string
        public string auteur { get; set; } // Déclaration de la propriété auteur de type string
        public int annee { get; set; } // Déclaration de la propriété annee de type int
        public string genre { get; set; } // Déclaration de la propriété genre de type string
        public string editeur { get; set; } // Déclaration de la propriété editeur de type string
        public string isbn { get; set; } // Déclaration de la propriété isbn de type string
        public string resume { get; set; } // Déclaration de la propriété resume de type string
        

        public Livre(string titre, string auteur, int annee, string genre, string editeur, string isbn, string resume) { // Déclaration du constructeur de la classe Livre
            
            this.titre = titre; // Initialisation de la propriété titre
            this.auteur = auteur; // Initialisation de la propriété auteur
            this.annee = annee; // Initialisation de la propriété annee
            this.genre = genre; // Initialisation de la propriété genre
            this.editeur = editeur; // Initialisation de la propriété editeur
            this.isbn = isbn; // Initialisation de la propriété isbn
            this.resume = resume; // Initialisation de la propriété resume
        }

        public void afficher() { // Déclaration de la méthode afficher de la classe Livre
            // Affichage des informations du livre (titre, auteur, année, genre, éditeur, ISBN, résumé)
            Console.WriteLine($"Titre: {titre}\nAuteur: {auteur}\nAnnée: {annee}\nGenre: {genre}\nEditeur: {editeur}\nISBN: {isbn}\nRésumé: {resume}\n"); 
        }
    }

    class Bibliotheque { // Déclaration de la classe Bibliotheque
        public List<Livre> livres = new List<Livre>(); // Déclaration de la propriété livres de type List<Livre> (création d'une liste de livres)

         public bool VerifierIsbn(string isbn) { // Déclaration de la méthode VerifierIsbn de la classe Bibliotheque (vérifie si un livre existe déjà dans la bibliothèque)
            return livres.Exists(l => l.isbn == isbn); // Retourne true si un livre avec le même ISBN existe déjà dans la bibliothèque, sinon false
        }

        public bool ValiderIsbn(string isbn) { // Déclaration de la méthode ValiderIsbn de la classe Bibliotheque (vérifie si un ISBN est valide)
            string pattern = @"^\d{3}-\d{1,5}-\d{1,7}-\d{1,7}-\d{1}$"; // Déclaration du pattern de l'ISBN (3 chiffres, 1 à 5 chiffres, 1 à 7 chiffres, 1 à 7 chiffres, 1 chiffre)
            return Regex.IsMatch(isbn, pattern); // Retourne true si l'ISBN est valide, sinon false
        }

        public bool VerifierAnnee(int annee) { // Déclaration de la méthode VerifierAnnee de la classe Bibliotheque (vérifie si l'année de publication est valide)
            int anneeactuelle = DateTime.Now.Year; // Récupère l'année en cours
            if (annee > anneeactuelle) // Vérifie si l'année de publication est supérieure à l'année en cours
            {
                return false; // Retourne false si l'année de publication est supérieure à l'année en cours
            }
            return true; // Retourne true si l'année de publication est valide
        }

        public void AjouterLivre(Livre livre) { // Déclaration de la méthode AjouterLivre de la classe Bibliotheque (pour ajouter un livre)
            if (VerifierIsbn(livre.isbn)) { // Vérifie si un livre avec le même ISBN existe déjà dans la bibliothèque (rappelle la méthode VerifierIsbn)
                Console.WriteLine("L'ISBN existe déjà dans la bibliothèque. Impossible d'ajouter ce livre.\n"); // Affiche un message d'erreur si c'est le cas
                return; // Retour au menu
            }

            if (!ValiderIsbn(livre.isbn)) { // Vérifie si l'ISBN n'est pas valide -> ! (rappelle la méthode ValiderIsbn)
                Console.WriteLine("L'ISBN n'est pas valide. Assurez-vous qu'il soit au bon format XXX-X-XX-XXXXX-X.\n"); // Affiche un message d'erreur si c'est le cas
                return; // Retour au menu 
            }
            if (!VerifierAnnee(livre.annee)) { // Vérifie si l'année de publication est supérieure à l'année en cours -> ! (rappelle la méthode VerifierAnnee)
                Console.WriteLine("Année invalide. L'année de publication ne peut pas être supérieure à l'année en cours.\n"); // Affiche un message d'erreur si c'est le cas
                return; // Retour au menu
            }
            livres.Add(livre); // Ajoute le livre à la liste de livres si l'ISBN est valide et n'existe pas déjà
            Console.WriteLine($"Livre \"{livre.titre}\" ajouté avec succès !\n"); // Affiche un message de succès
        }

        public void SupprimerLivre(string titre) { // Déclaration de la méthode SupprimerLivre de la classe Bibliotheque (pour supprimer un livre)
            Livre livreASupprimer = livres.Find(l => l.titre.Equals(titre, StringComparison.OrdinalIgnoreCase)); // Recherche le livre à supprimer par titre (ignorant la casse)
            if (livreASupprimer != null) { // Vérifie si le livre à supprimer existe
                livres.Remove(livreASupprimer); // Si c'est le cas, on supprime le livre de la liste de livres
                Console.WriteLine($"Livre \"{titre}\" supprimé avec succès !\n"); // Affiche un message de succès
            } else { // Si le livre à supprimer n'existe pas
                Console.WriteLine($"Livre \"{titre}\" introuvable.\n"); // Affiche un message d'erreur
            }
        }

        public void RechercherLivre(string recherche) { // Déclaration de la méthode RechercherLivre de la classe Bibliotheque (pour rechercher un livre)
            var resultats = livres.FindAll(l => l.titre.Contains(recherche, StringComparison.OrdinalIgnoreCase) || 
                                                l.auteur.Contains(recherche, StringComparison.OrdinalIgnoreCase) ||
                                                l.annee.ToString().Contains(recherche, StringComparison.OrdinalIgnoreCase)); // Recherche par titre, auteur ou année (ignorant la casse)
            if (resultats.Count > 0) { // Vérifie si des livres ont été trouvés
                Console.WriteLine("Livres trouvés :\n"); // Affiche un message de succès si c'est le cas
                foreach (var livre in resultats) { // Affiche les livres trouvés pour chaque livre du resultat de la recherche
                    livre.afficher();  // Affiche les informations du livre
                }
            } else { // Si aucun livre n'a été trouvé
                Console.WriteLine($"Aucun livre trouvé pour la recherche \"{recherche}\".\n"); // Affiche un message d'erreur
            }
        }

        public void AfficherTout() { // Déclaration de la méthode AfficherTout de la classe Bibliotheque (pour afficher tous les livres)
            if (livres.Count > 0) { // Si il y a au moins un livre dans la bibliothèque
                Console.WriteLine("Liste des livres :\n"); // Liste des livres : 
                foreach (var livre in livres) { // Affiche les livres trouvés pour chaque livre dans livres
                    livre.afficher(); // Affiche les informations du livre (appel de la méthode afficher)
                }
            } else { // Si il n'y a aucun livre dans la bibliothèque
                Console.WriteLine("Aucun livre dans la bibliothèque.\n"); // Affiche un message d'erreur
            }
        }

        public void Sauvegarder(string cheminFichier) { // Déclaration de la méthode Sauvegarder de la classe Bibliotheque
            try { // Essaie de sauvegarder la bibliothèque au format JSON
                string json = System.Text.Json.JsonSerializer.Serialize(livres, new JsonSerializerOptions { WriteIndented = true }); // Sérialisation de la liste de livres en JSON (utilisation de System.Text.Json)
                File.WriteAllText(cheminFichier, json); // Écriture du fichier JSON
                Console.WriteLine("Bibliothèque sauvegardée avec succès au format JSON !\n"); // Affiche un message de succès
            }   
            catch (Exception e) { // Si il y a une erreur quelconque dans la sauvegarde
                Console.WriteLine($"Erreur lors de la sauvegarde : {e.Message}\n"); // Affiche un message d'erreur
            }
        }


        public void Charger(string cheminFichier) { // Déclaration de la méthode Charger de la classe Bibliotheque
            try { // Essaie de charger la bibliothèque depuis un fichier JSON
                if (File.Exists(cheminFichier)) { // Si le fichier existe 
                    string json = File.ReadAllText(cheminFichier); // Lecture du fichier JSON
                    livres = System.Text.Json.JsonSerializer.Deserialize<List<Livre>>(json) ?? new List<Livre>(); // Désérialisation de la liste de livres depuis le fichier JSON (utilisation de System.Text.Json)
                    Console.WriteLine("Bibliothèque chargée avec succès depuis le fichier JSON !\n"); // Affiche un message de succès
                } else { // Si le fichier n'existe pas 
                    Console.WriteLine("Fichier JSON introuvable.\n"); // Affiche un message d'erreur
                }
            }   
            catch (Exception e) { // Si il y a une erreur quelconque dans le chargement
            Console.WriteLine($"Erreur lors du chargement : {e.Message}\n"); // Affiche un message d'erreur
            }
        }

    }

    class Program { // Déclaration de la classe Program (classe principale)
        static void Main() { // Déclaration de la méthode Main (méthode principale)
            Bibliotheque bibliotheque = new Bibliotheque(); // Création d'une instance de la classe Bibliotheque
            string cheminFichier = "bibliotheque.json"; // Déclaration du chemin du fichier JSON (ici a la racine du projet donc simplement le nom du fichier)

            while (true) { // Tant que la méthode Main est exécutée (donc tant que l'utilisateur exécute le programme)
                Console.WriteLine("--- Menu ---"); // Début du menu 
                Console.WriteLine("1. Ajouter un livre"); // Option 1 : Ajouter un livre
                Console.WriteLine("2. Supprimer un livre"); // Option 2 : Supprimer un livre
                Console.WriteLine("3. Rechercher un livre"); // Option 3 : Rechercher un livre
                Console.WriteLine("4. Afficher tous les livres"); // Option 4 : Afficher tous les livres
                Console.WriteLine("5. Sauvegarder la bibliothèque"); // Option 5 : Sauvegarder la bibliothèque
                Console.WriteLine("6. Charger la bibliothèque"); // Option 6 : Charger la bibliothèque
                Console.WriteLine("7. Quitter"); // Option 7 : Quitter
                Console.Write("Choisissez une option (1-7): "); // Demande à l'utilisateur de choisir une option

                string choix = Console.ReadLine(); // Lecture de l'option choisie par l'utilisateur
                switch (choix) { // Switch case pour chaque option
                    case "1": // Option 1 : Ajouter un livre
                        Console.Write("Entrez le titre du livre: "); // Demande à l'utilisateur d'entrer le titre du livre
                        string titre = Console.ReadLine(); // Lecture du titre du livre
                        Console.Write("Entrez l'auteur du livre: "); // Demande à l'utilisateur d'entrer l'auteur du livre
                        string auteur = Console.ReadLine(); // Lecture de l'auteur du livre
                        Console.Write("Entrez l'année de publication: "); // Demande à l'utilisateur d'entrer l'année de publication
                        int annee = int.Parse(Console.ReadLine()); // Lecture de l'année de publication
                        Console.Write("Entrez le genre du livre: "); // Demande à l'utilisateur d'entrer le genre du livre
                        string genre = Console.ReadLine(); // Lecture du genre du livre
                        Console.Write("Entrez l'éditeur du livre: "); // Demande à l'utilisateur d'entrer l'éditeur du livre
                        string editeur = Console.ReadLine(); // Lecture de l'éditeur du livre
                        Console.Write("Entrez l'ISBN du livre (au format XXX-X-XX-XXXXX-X): "); // Demande à l'utilisateur d'entrer l'ISBN du livre
                        string isbn = Console.ReadLine(); // Lecture de l'ISBN du livre
                        Console.Write("Entrez un résumé du livre: "); // Demande à l'utilisateur d'entrer un résumé du livre
                        string resume = Console.ReadLine(); // Lecture du résumé du livre

                        Livre livre = new Livre(titre, auteur, annee, genre, editeur, isbn, resume); // Création d'une instance de la classe Livre
                        bibliotheque.AjouterLivre(livre); // Ajout du livre à la bibliothèque (avec la méthode AjouterLivre) 
                        break; // Fin de l'option 1

                    case "2": // Option 2 : Supprimer un livre
                        Console.Write("Entrez le titre du livre à supprimer: "); // Demande à l'utilisateur d'entrer le titre du livre à supprimer
                        string titreASupprimer = Console.ReadLine(); // Lecture du titre du livre à supprimer
                        bibliotheque.SupprimerLivre(titreASupprimer); // Suppression du livre de la bibliothèque (avec la méthode SupprimerLivre)
                        break; // Fin de l'option 2

                    case "3": // Option 3 : Rechercher un livre
                        Console.Write("Entrez le titre, l'auteur ou la date à rechercher: "); // Demande à l'utilisateur d'entrer le titre, l'auteur ou la date à rechercher
                        string recherche = Console.ReadLine(); // Lecture de la recherche
                        bibliotheque.RechercherLivre(recherche); // Recherche du livre dans la bibliothèque (avec la méthode RechercherLivre)
                        break; // Fin de l'option 3

                    case "4": // Option 4 : Afficher tous les livres
                        bibliotheque.AfficherTout(); // Affichage de tous les livres de la bibliothèque (avec la méthode AfficherTout)
                        break; // Fin de l'option 4

                    case "5": // Option 5 : Sauvegarder la bibliothèque
                        bibliotheque.Sauvegarder(cheminFichier); // Sauvegarde de la bibliothèque (avec la méthode Sauvegarder)
                        break; // Fin de l'option 5

                    case "6": // Option 6 : Charger la bibliothèque
                        bibliotheque.Charger(cheminFichier); // Chargement de la bibliothèque (avec la méthode Charger)
                        break; // Fin de l'option 6

                    case "7": // Option 7 : Quitter
                        Console.WriteLine("Salut mon pote !"); // Petit message d'adieu qui fait plaisir
                        return; // Fin de la méthode main et donc fin de l'execution du code

                    default: // Par défaut (l'utilisateur ne tape pas un chiffre entre 1 et 7)
                        Console.WriteLine("Option invalide, veuillez réessayer."); // Message d'avertissement
                        break; // Retour au choix dans le menu
                }

                Console.WriteLine(); // Ajout d'une ligne vide
            }
        }
    }
}

