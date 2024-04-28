import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  // @Input() usersFromHomeComponent: any; //Recebemos do pai (template)
  @Output() cancelRegister = new EventEmitter(); //child to parent
  registerUserForm: FormGroup = new FormGroup({});
  maxDate: Date = new Date();
  validationErrors: string[] | undefined;

  constructor(private accountService: AccountService, private toastr: ToastrService, private fb: FormBuilder, private router: Router) { }

  ngOnInit(): void {
    this.initializeForm();
    this.maxDate.setFullYear(this.maxDate.getFullYear() - 18);

  }

  initializeForm() {
    this.registerUserForm = this.fb.group({
      gender: ['male'],
      username: ['', Validators.required],
      knownAs: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
      city: ['', Validators.required],
      country: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(8)]],
      confirmPassword: ['', [Validators.required, this.matchValues('password')]],
    });

    //validate everytime the password is changed....
    this.registerUserForm.controls['password'].valueChanges.subscribe({
      next: () => this.registerUserForm.controls['confirmPassword'].updateValueAndValidity()
    })
  }

  matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => { //control that we are working with (form control) everything from reactives form derives from AbstractControl
      //Adding an error to return (or null)
      //if match, return null, if not return error with prop name notMatching
      return control.value === control.parent?.get(matchTo)?.value ? null : { notMatching: true };
    }
  }

  register() {
    const dob = this.getDateOnly(this.registerUserForm.controls['dateOfBirth'].value);
    const values = {...this.registerUserForm.value, dateOfBirth: dob}; //set dateOfBirth from form as const dob using spread operator
    this.accountService.register(values).subscribe({
      next: () => {
        this.router.navigateByUrl('/members');
      },
      error: error => {
        this.validationErrors = error;
      }
    })
  }

  cancel() {
    this.cancelRegister.emit(false); //emitindo valor false pra tornar o registerMode como false no parent
  }

  private getDateOnly(dob: string | undefined){//gets from form control as string
    if (!dob) return;
    let theDob = new Date(dob); //date object to work with.
    return new Date(theDob.setMinutes(theDob.getMinutes()-theDob.getTimezoneOffset())).toISOString().slice(0,10); //slice char 0 to 10.. 
  }
}
