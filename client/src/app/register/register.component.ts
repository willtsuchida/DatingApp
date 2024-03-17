import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  // @Input() usersFromHomeComponent: any; //Recebemos do pai (template)
  @Output() cancelRegister = new EventEmitter(); //child to parent

  registerUserForm: FormGroup = new FormGroup({});
  model: any = {}

  constructor(private accountService: AccountService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.registerUserForm = new FormGroup({
      username: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required)
    })
  }

  register() {
    this.model = {
      username: this.registerUserForm.get('username')?.value,
      password: this.registerUserForm.get('password')?.value
    };
    this.accountService.register(this.model).subscribe({
      next: () => {
        this.cancel();
      },
      error: error => {
        this.toastr.error(error.error)
        console.log(error)
      }
    })
  }

  cancel() {
    this.cancelRegister.emit(false); //emitindo valor false pra tornar o registerMode como false no parent
  }
}
