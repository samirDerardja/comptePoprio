import { Component, OnInit } from '@angular/core';
import { RepositoryService } from 'src/app/shared/repository.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ErrorHandlerService } from 'src/app/shared/error-handler.service';
import { Proprietaire } from '../_interface-proprietaire/proprietaire.model';

@Component({
  selector: 'app-proprietaire-details',
  templateUrl: './proprietaire-details.component.html',
  styleUrls: ['./proprietaire-details.component.css']
})
export class ProprietaireDetailsComponent implements OnInit {

  public proprietaire: Proprietaire;
  public voirComptes: any;

  // tslint:disable-next-line: max-line-length
  constructor(private repoService: RepositoryService, private router: Router,
              private activeRoute: ActivatedRoute, private error: ErrorHandlerService) { }

  ngOnInit(): void {
    this.getOwnerDetails();
  }

  private getOwnerDetails = () => {
    const id: string = this.activeRoute.snapshot.params.id;
    const apiUrl = `api/owner/${id}/account`;

    this.repoService.getData(apiUrl)
      .subscribe(res => {
        this.proprietaire = res as Proprietaire;
        console.log(this.proprietaire);
      },
        (error) => {
          this.error.handleError(error);
        });
  }

}
