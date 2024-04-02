import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Observable, of } from 'rxjs';
import { User } from '../_models/user.model';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
  // currentUser$: Observable<User | null> = of(null); Não precisa ficar repetindo, esse observable ja existe no Service, e ele ta injetado aqui.
  myForm: FormGroup = new FormGroup({});

  constructor(public accountService: AccountService, private router: Router, private toastr: ToastrService) { } //tem que ser public pra usar o observable no template

  ngOnInit(): void {
    this.myForm = new FormGroup({
      username: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required)
    });

    // this.currentUser$ = this.accountService.currentUser$; Usar o do Service
  }


  login() {
    this.model = { username: this.myForm.get('username')?.value, password: this.myForm.get('password')?.value }
    console.log(this.model);
    this.accountService.login(this.model).subscribe({
      next: _ => this.router.navigateByUrl('/members'), //eh comum user o underscore qnd n usa, msm coisa q ()
      // error: error => {this.toastr.error(error.error)}  --> Agora esta sendo handled pelo Interceptor
    })
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }

}