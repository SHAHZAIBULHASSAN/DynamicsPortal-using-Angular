import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { environment } from 'src/environments/environment';
import { Lead } from '../models';
@Component({
  selector: 'app-lead',
  templateUrl: './lead.component.html',
  styles: [
  ]
})
export class LeadComponent implements OnInit {
  id:string;
  isNew:boolean;
  loading:boolean=true;
  processing:boolean=false;
  deleting:boolean=false;
  form!:FormGroup;
  constructor(private route: ActivatedRoute,
    private router:Router,
    private http:HttpClient,
    private fb: FormBuilder) {
    this.id = route.snapshot.params.id;
    this.isNew = this.id.toLowerCase() == "new";
  }

  ngOnInit(): void {
    this.form = this.fb.group({
      "id":[null, []],
      "firstName":[null, [Validators.required]],
      "lastName":[null, [Validators.required]],
      "subject":[null, [Validators.required]],
      "emailAddress":[null, [Validators.email]],
      "Telephone1":[null, [Validators.required]],
    });

    if(this.isNew){
      this.loading = false;
    }else {
      this.http.get<Lead>(`${environment.apiUrl}/lead/${this.id}`)
      .subscribe(res => this.form.patchValue(res)).add(() => this.loading = false);
    }
  }

  submit() {
    this.processing = true;
    if(this.isNew){
      this.http.post<Lead>(`${environment.apiUrl}/lead`, this.form.value)
      .subscribe(res => this.router.navigate(['/leads']));
    }else{
      this.http.put<Lead>(`${environment.apiUrl}/lead`, this.form.value)
      .subscribe(res => this.router.navigate(['/leads']));
    }
  }
  delete() {
    if(!confirm('Are you sure to delete?')) return;
    this.deleting = true;
    this.http.delete(`${environment.apiUrl}/lead/${this.form.value.id}`).subscribe(() => this.router.navigate(['/leads']));
  }

}
