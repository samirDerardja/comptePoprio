import { Component, OnInit, Input } from '@angular/core';
import { Compte } from 'src/app/comptes/_interface-compte/compte.model';

@Component({
  selector: 'app-compte-data',
  templateUrl: './compte-data.component.html',
  styleUrls: ['./compte-data.component.css']
})
export class CompteDataComponent implements OnInit {

  @Input() public accounts: Compte[];

  constructor() { }

  ngOnInit(): void {
  }

}
