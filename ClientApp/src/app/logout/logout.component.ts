import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styles: [
  ]
})
export class LogoutComponent implements OnInit {
  loggingOut:boolean=false;
  constructor() { }

  ngOnInit(): void {
  }

  doLogout = () => { this.loggingOut = true; document.location.assign('/connect/logout'); }

}
