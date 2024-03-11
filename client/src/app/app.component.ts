import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Dating App';

  users: any;

  constructor(private http: HttpClient) { } //1 a ser executado, aki eh mto cedo pra pegar dados

  ngOnInit(): void {
    this.http.get<any>('https://localhost:5001/api/users').subscribe({
      //pegamos de volta um Objeto do Tipo OBSERVER
      next: response => this.users = response,
      error: error => console.log(error),
      complete: () => console.log('Request has completed')      
    });

  }


}
