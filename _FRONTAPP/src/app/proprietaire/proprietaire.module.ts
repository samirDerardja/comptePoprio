import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProprietaireListComponent } from './proprietaire-list/proprietaire-list.component';
import { ProprietaireRoutingModule } from './proprietaire-routing/proprietaire-routing.module';
import { MaterialModule } from './../material/material.module';
import { ProprietaireDetailsComponent } from './proprietaire-details/proprietaire-details.component';
import { ProprietaireDataComponent } from './proprietaire-details/proprietaire-data/proprietaire-data.component';
import { CompteDataComponent } from './proprietaire-details/compte-data/compte-data.component';
import { ProprietaireCreateComponent } from './proprietaire-create/proprietaire-create.component';
import { ReactiveFormsModule } from '@angular/forms';
// ! on en profite egalment pour factoriser les modules par composents afin de bien les separer

@NgModule({
  declarations: [
     ProprietaireListComponent,
     ProprietaireDetailsComponent,
     ProprietaireDataComponent,
     CompteDataComponent,
     ProprietaireCreateComponent],
  imports: [
    CommonModule,
    ProprietaireRoutingModule,
    MaterialModule,
    ReactiveFormsModule
  ]
})
export class ProprietaireModule { }
