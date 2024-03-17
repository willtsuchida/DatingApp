import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Observable, of } from 'rxjs';
import { User } from '../_models/user.model';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
  // currentUser$: Observable<User | null> = of(null); Não precisa ficar repetindo, esse observable ja existe no Service, e ele ta injetado aqui.
  myForm: FormGroup = new FormGroup({});

  constructor(public accountService: AccountService) { } //tem que ser public pra usar o observable no template

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
      next: response => {
        console.log(response);
      },
      error: error => console.log(error)
    });
  }

  logout() {
    this.accountService.logout();
  }

}