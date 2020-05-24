import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { ProprietaireListComponent } from '../proprietaire-list/proprietaire-list.component';
import { ProprietaireDetailsComponent } from '../proprietaire-details/proprietaire-details.component';
import { ProprietaireCreateComponent } from '../proprietaire-create/proprietaire-create.component';

const routes: Routes = [
  { path: 'proprietaires', component: ProprietaireListComponent },
  { path: 'details/:id', component: ProprietaireDetailsComponent},
  { path: 'creer', component: ProprietaireCreateComponent }
];


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  exports : [
   RouterModule
  ]
})
export class ProprietaireRoutingModule { }
