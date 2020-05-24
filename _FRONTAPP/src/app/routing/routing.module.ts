import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from '../home/home.component';
import { NotFoundComponent } from '../error-pages/not-found/not-found.component';
import { ServerErrorComponent } from '../error-pages/server-error/server-error.component';

const routes: Routes = [
  { path: 'acceuil', component: HomeComponent},
  { path: 'proprietaire', loadChildren: () => import('../proprietaire/proprietaire.module').then(m => m.ProprietaireModule) },
  { path: '404', component: NotFoundComponent },
  { path: '500', component: ServerErrorComponent },
  { path: '', redirectTo: '/acceuil', pathMatch: 'full' },
  { path: '**', redirectTo: '/404', pathMatch: 'full'}

];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forRoot(routes)
  ],
  exports: [
    RouterModule
  ],
  declarations: []
})
export class RoutingModule { }
