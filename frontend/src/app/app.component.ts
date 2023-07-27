import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import PassengerItem from './PassengerItem';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  public data: PassengerItem[] = [
   // { id: 011023086453, lastName: 'Jessiman', firstName: 'Sarah', nationality: 'South African' },
  ];

  httpHeaders = new HttpHeaders()
    .set('Access-Control-Allow-Origin', '*')
    .set('Content-Type', 'application/json');

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.getData();
  }

  getData() {
    let url = `${environment.baseUrl}/api/PassengerItems`;

    this.http
      .get<PassengerItem[]>(url, { headers: this.httpHeaders })
      .subscribe((result) => {
        this.data = [...this.data, ...result];
      });
  }

  submitData() {
    let url = `${environment.baseUrl}/api/PassengerItems`;

    const id = document.getElementById(
      'submitPassengerItemId'
    ) as HTMLInputElement;

    const lastName = document.getElementById(
      'submitPassengerItemLastName'
    ) as HTMLInputElement;

    const firstName = document.getElementById(
      'submitPassengerItemFirstName'
    ) as HTMLInputElement;

    const nationality = document.getElementById(
      'submitPassengerItemNationality'
    ) as HTMLInputElement;


    let data: PassengerItem = {
      id: parseInt(id.value),
      lastName: lastName.value,
      firstName: firstName.value,
      nationality: nationality.value,
    };

    this.http
      .post<PassengerItem>(url, data, { headers: this.httpHeaders })
      .subscribe((result) => {
        this.data = [...this.data, result];
      });
  }

  updateData() {

    const id = document.getElementById(
      'updatePassengerItemId'
    ) as HTMLInputElement;

    const lastName = document.getElementById(
      'updatePassengerItemLastName'
    ) as HTMLInputElement;

    const firstName = document.getElementById(
      'updatePassengerItemFirstName'
    ) as HTMLInputElement;

    const nationality = document.getElementById(
      'updatePassengerItemNationality'
    ) as HTMLInputElement;

    let url = `${environment.baseUrl}/api/PassengerItems/${id.value}`;

    let data: PassengerItem = {
      id: parseInt(id.value),
      lastName: lastName.value,
      firstName: firstName.value,
      nationality: nationality.value,   
    };

    this.http
      .put<any>(url, data, { headers: this.httpHeaders })
      .subscribe((result) => {
        this.updateItem(id.valueAsNumber, data);
      });
  }

  updateItem(id: number, newData: PassengerItem) {
    let index = this.data.findIndex((el) => el.id == id);

    if (index != null) {
      this.data[index] = newData;
    }
  }

  deleteData() {
    const id = document.getElementById(
      'deletePassengerItemId'
    ) as HTMLInputElement;

    let url = `${environment.baseUrl}/api/PassengerItems/${id.value}`;

    this.http
      .delete<any>(url, { headers: this.httpHeaders })
      .subscribe((result) => {
        this.deleteItem(id.valueAsNumber);
      });
  }

  deleteItem(id: number) {
    let index = this.data.findIndex((el) => el.id == id);

    if (index != null) {
      delete this.data[index];
    }
  }
}