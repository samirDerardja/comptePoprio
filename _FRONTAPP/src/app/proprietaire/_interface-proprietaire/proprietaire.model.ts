
import { Compte } from '../../comptes/_interface-compte/compte.model';

export interface Proprietaire {
  id: string;
  nom: string;
  dateDeNaissance: Date;
  adresse: string;
  accounts?: Compte[] ;


}
