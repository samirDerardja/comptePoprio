export interface Compte {
  id: string;
  dateDeCreation: Date;
  typeDeCompte: string;
  idProprietaire?: string;
}
