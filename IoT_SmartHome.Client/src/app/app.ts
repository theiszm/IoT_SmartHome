import { Component, OnInit, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SmartLight, SmartThermostat, SmartSecurityCamera, SmartSpeaker } from './smart-device.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  standalone: false,
  styleUrl: './app.css'
})

export class AppComponent implements OnInit {
  protected readonly title = signal('IoT_SmartHome.Client');

  public lights: SmartLight[] = [];
  public isLoading: boolean = true;

  // inject HttpClient into constructor
  constructor(private http: HttpClient) {}
  
  ngOnInit() {
	  this.getLights();
  }

  // fetches data from the .NET API
  // "subscribe" is equivalent to Task and await in C#
  getLights() {
    console.log("Fetching lights...");
    this.isLoading = true;
	  this.http.get<SmartLight[]>('https://localhost:7016/api/smartlights').subscribe({
		  next: (result) => {
        this.lights = result;
        this.isLoading = false; // turn off when data arrives
		  },
      error: (err) => {
        this.isLoading = false;
			  console.error('API Error:', err);
		  }
	  });
  }
}
