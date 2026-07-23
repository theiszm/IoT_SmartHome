import { Component, OnInit, signal, ChangeDetectorRef } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { forkJoin } from 'rxjs';
import { SmartLight, SmartThermostat, SmartSecurityCamera, SmartSpeaker, SmartLock } from './smart-device.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  standalone: false,
  styleUrl: './app.css'
})
export class AppComponent implements OnInit {
  protected readonly title = signal('IoT_SmartHome.Client');

  public lights: SmartLight[] = [];
  public thermostats: SmartThermostat[] = [];
  public cameras: SmartSecurityCamera[] = [];
  public speakers: SmartSpeaker[] = [];
  public locks: SmartLock[] = [];

  public isLoading: boolean = true;
  private readonly baseUrl = 'http://localhost:5279/api';

  // Injected ChangeDetectorRef (cdr) into constructor
  constructor(private http: HttpClient, private cdr: ChangeDetectorRef) { }

  ngOnInit() {
    this.fetchDashboardData();
  }

  fetchDashboardData() {
    this.isLoading = true;

    forkJoin({
      lights: this.http.get<SmartLight[]>(`${this.baseUrl}/smartlights`),
      thermostats: this.http.get<SmartThermostat[]>(`${this.baseUrl}/smartthermostats`),
      cameras: this.http.get<SmartSecurityCamera[]>(`${this.baseUrl}/smartsecuritycameras`),
      speakers: this.http.get<SmartSpeaker[]>(`${this.baseUrl}/smartspeakers`),
      locks: this.http.get<SmartLock[]>(`${this.baseUrl}/smartlocks`)
    }).subscribe({
      next: (response) => {
        this.lights = response.lights;
        this.thermostats = response.thermostats;
        this.cameras = response.cameras;
        this.speakers = response.speakers;
        this.locks = response.locks;
        this.isLoading = false;

        // Forces Angular to run change detection and clear the spinner
        this.cdr.detectChanges();
      },
      error: (err) => {
        console.error('Failed to load dashboard devices:', err);
        this.isLoading = false;
        this.cdr.detectChanges(); // Forces UI to show empty error state if backend drops
      },
      complete: () => {
        this.isLoading = false;
        this.cdr.detectChanges();
      }
    });
  }
}
