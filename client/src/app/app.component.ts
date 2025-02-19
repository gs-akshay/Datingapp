import { NgFor } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet,NgFor],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit{
  http=inject(HttpClient);
  title = 'Datingapp';
  users: any;
  

  ngOnInit(): void {
    this.http.get('http://localhost:5180/api/users').subscribe({
      next: response => this.users=response,
      error: error => console.log(error),
      complete: () => console.log('Request has completed')

    })
  }
}
