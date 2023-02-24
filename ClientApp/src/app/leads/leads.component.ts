import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';
import { Lead } from '../models';

@Component({
  selector: 'app-leads',
  templateUrl: './leads.component.html',
  styles: [
  ]
})
export class LeadsComponent implements OnInit {
  loading:boolean = true;
  list:Lead[] = [];
  constructor(private api:ApiService) {}

  ngOnInit(): void {
    this.loading = true;
    this.api.getLeads().subscribe(res => this.list = res).add(() => this.loading = false);
  }
}
