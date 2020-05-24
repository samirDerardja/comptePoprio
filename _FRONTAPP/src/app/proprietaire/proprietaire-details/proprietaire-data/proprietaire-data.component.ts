import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Proprietaire } from '../../_interface-proprietaire/proprietaire.model';

@Component({
  selector: 'app-proprietaire-data',
  templateUrl: './proprietaire-data.component.html',
  styleUrls: ['./proprietaire-data.component.css']
})
export class ProprietaireDataComponent implements OnInit {

  @Input() public proprietaire: Proprietaire;
  public selectOptions = [{name:'Voir', value: 'voir'}, {name: `Cacher`, value: ''}];
  @Output() selectEmitt = new EventEmitter();

  constructor() { }

  ngOnInit(): void {
  }

  public onChange = (event) => {
    this.selectEmitt.emit(event.value);
  }

}
