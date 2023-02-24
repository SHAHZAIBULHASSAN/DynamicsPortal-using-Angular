import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LeadComponent } from './lead/lead.component';
import { LeadsComponent } from './leads/leads.component';
import { LogoutComponent } from './logout/logout.component';
import { WelcomeComponent } from './welcome/welcome.component';

const routes: Routes = [
  { path: '', redirectTo: '/welcome', pathMatch: 'full' },
  { path: 'welcome', component: WelcomeComponent },
  { path: 'leads', component: LeadsComponent },
  { path: 'lead/:id', component: LeadComponent },
  { path: 'lead', redirectTo: '/leads', pathMatch: 'full' },
  { path: 'logout', component: LogoutComponent },
  { path: '**', redirectTo: '/welcome' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
