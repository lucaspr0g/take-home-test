import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { Loan } from './loan';
import { Token } from './token';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  apiUrl = 'http://localhost:60992/';
  token = '';
  loans: Loan[] = [];
  displayedColumns: string[] = [
    'loanAmount',
    'currentBalance',
    'applicant',
    'status',
  ];

  http = inject(HttpClient);

  constructor() {
    this.getToken().subscribe({
      next: (token) => {
        this.token = token.token;
        console.log('token retrieved');

        this.getLoans().subscribe({
          next: (loans) => {
            this.loans = loans;
          },
          error: (err) => {
            console.error('Error fetching loans:', err);
          }
        });
      },
      error: (err) => {
        console.error('Error fetching token:', err);
      }
    });
  }

  getLoans(): Observable<Loan[]> {
    const headers = new HttpHeaders({
      'Authorization': 'Bearer ' + this.token
    });

    return this.http.get<Loan[]>(this.apiUrl + 'loans', { headers: headers });
  }

  getToken(): Observable<Token> {
    const headers = new HttpHeaders({
      'ClientId': '9d2b1b34f3679601737cc7e3eddaf01f'
    });
    
    return this.http.post<Token>(this.apiUrl + 'auth', null, { headers: headers });
  }
}
