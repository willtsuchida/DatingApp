import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { User } from '../_models/user.model';

@Injectable({       // Decorator, Angular services can be injected into components or other services
  providedIn: 'root'
})
export class AccountService {

  baseUrl: string = 'https://localhost:5001/api/';
  //Behaviour subject to monitor if user is logged in
  //its an special kind of observable, it allows an observable to have an initial value that we can use elsewhere in the app
  private currentUserSource = new BehaviorSubject<User | null>(null); //Union type
  currentUser$ = this.currentUserSource.asObservable();


  //Services are SINGLETON

  //why use service? Centralize http requests; Services are in root, so they are initialized when the app is initalized until its closed (browser)
  // If u use services in component, it might me destroyed when the component is destroyed (ie on a page change...) and you lose all data.
  constructor(private http: HttpClient) { }

  login(model: User) {
    return this.http.post<User>(this.baseUrl + 'account/login', model).pipe( /* post<User> --> UserDto na APi*/
      map((response: User) => {
        const user = response;
        if (user) {
          localStorage.setItem('user', JSON.stringify(user)); // Guardo variavel na memoria local do browser
          this.currentUserSource.next(user);
        }
      })
    )
  }

  register(model: any) {
    return this.http.post<User>(this.baseUrl + 'account/register', model)

      .pipe(
        map((user) => {
          if (user) {
            localStorage.setItem('user', JSON.stringify(user));
            this.currentUserSource.next(user);
          }
          // return user; // usando projecao com Map, se nao retornar o user aqui, retornaria undefined no console.log
          //nao vamos usar isso, so foi exemplo
        })
      )
  }

  setCurrentUser(user: User) {
    this.currentUserSource.next(user);
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }
}
