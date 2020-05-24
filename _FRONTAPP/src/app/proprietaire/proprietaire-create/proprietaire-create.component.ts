import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { RepositoryService } from 'src/app/shared/repository.service';
import { Location } from '@angular/common';
import { CreationProprietaire } from '../_interface-proprietaire/creationProprietaire';
import { MatDialog } from '@angular/material/dialog';
import { SuccessDialogComponent } from 'src/app/shared/dialogs/success-dialog/success-dialog.component';
import { ErrorHandlerService } from '../../shared/error-handler.service';

@Component({
  selector: 'app-proprietaire-create',
  templateUrl: './proprietaire-create.component.html',
  styleUrls: ['./proprietaire-create.component.css']
})
export class ProprietaireCreateComponent implements OnInit {

  public proprietaireGroup: FormGroup;
  private dialogConfig : any;

  constructor(private location: Location, private repoService: RepositoryService,
     private dialog: MatDialog, private errorService: ErrorHandlerService) { }

  ngOnInit(): void {

      this.proprietaireGroup = new FormGroup({
      nom: new FormControl('', [Validators.required, Validators.maxLength(60)]),
      dateDeNaissance: new FormControl(new Date()),
      adresse: new FormControl('', [Validators.required, Validators.maxLength(100)])
    });

      this.dialogConfig = {
      height: '250px',
      width: '400px',
      disableClose: true,
      data: { }
    };

  }


  public hasError = (controlName: string, errorName: string) =>{
    return this.proprietaireGroup.controls[controlName].hasError(errorName);
  }

  public onCancel = () => {
    this.location.back();
  }

  public createOwner = (proprietaireGroupValue) => {
    if (this.proprietaireGroup.valid) {
      this.executeOwnerCreation(proprietaireGroupValue);
    }
  }

  private executeOwnerCreation = (proprietaireGroupValue: { nom: any; dateDeNaissance: any; adresse: any; }) => {

    const owner: CreationProprietaire = {
      nom: proprietaireGroupValue.nom,
      dateDeNaissance: proprietaireGroupValue.dateDeNaissance,
      adresse: proprietaireGroupValue.adresse
    };

    const apiUrl = 'api/owner';
    this.repoService.create(apiUrl, owner)
      .subscribe(res => {
        const dialogRef = this.dialog.open(SuccessDialogComponent, this.dialogConfig);

        // we are subscribing on the [mat-dialog-close] attribute as soon as we click on the dialog button
        dialogRef.afterClosed()
          .subscribe(result => {
            this.location.back();
          });
      },
      (error => {
        this.errorService.dialogConfig = { ...this.dialogConfig };
        this.errorService.handleError(error);
      })
    )
  }

}
