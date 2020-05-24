import { Component, OnInit, AfterViewInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Proprietaire } from '../_interface-proprietaire/proprietaire.model';
import { RepositoryService } from 'src/app/shared/repository.service';
import { MatSort, Sort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { ErrorHandlerService } from '../../shared/error-handler.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-proprietaire-list',
  templateUrl: './proprietaire-list.component.html',
  styleUrls: ['./proprietaire-list.component.css']
})
export class ProprietaireListComponent implements OnInit , AfterViewInit {

  public displayedColumns = ['nom', 'dateDeNaissance', 'adresse', 'details', 'update', 'delete'];

  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;

  public dataSource = new MatTableDataSource<Proprietaire>();

  constructor(private repoService: RepositoryService, private errorHandlerService: ErrorHandlerService, private router: Router) { }

  ngOnInit(): void {
    this.getAllOwners();
  }

// ! ngAfter sera initialisé juste apres le ngInit, c' est pour cela que nous avons besoin du viewChlid
  ngAfterViewInit(): void {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
  }

  public getAllOwners = () => {
    this.repoService.getData('api/owner')
    .subscribe(res => {
      this.dataSource.data = res as Proprietaire[];
    },
    (error) => {
      this.errorHandlerService.handleError(error);
    });
  }

  public redirectToDetails = (id: string) => {
    const url = `/proprietaire/details/${id}`;
    this.router.navigate([url]);
  }

  public redirectToUpdate = (id: string) => {

  }

  public redirectToDelete = (id: string) => {

  }

  public customSort = (event) => {
    const sortState: Sort = {active: 'nom', direction: 'desc'};
    const dateState: Sort = {active: 'dateDeNaissance', direction: 'asc'};
    console.log(dateState);
    console.log(sortState);
  }

  public doFilter = (value: string) => {
    this.dataSource.filter = value.trim().toLocaleLowerCase();
  }



}
