import { Injectable } from '@angular/core';
import { Router, NavigationStart } from '@angular/router';
import { Observable } from 'rxjs';
import { Subject } from 'rxjs/Subject';
import 'rxjs/add/operator/filter';

import { Alert, AlertType } from '../models/alert';

@Injectable()
export class AlertService {
    private subject = new Subject<Alert>();
    private keepAfterRouteChange = false;
    alerts: Alert[] = [];

    constructor(private router: Router) {
        // clear alert messages on route change unless 'keepAfterRouteChange' flag is true
        router.events.subscribe(event => {
            if (event instanceof NavigationStart) {
                if (this.keepAfterRouteChange) {
                    // only keep for a single route change
                    this.keepAfterRouteChange = false;
                } else {
                    // clear alert messages
                    this.clear();
                }
            }
        });
    }
    ngOnInit() {
        // this.alertService.getAlert().subscribe((alert: Alert) => {
        //     if (!alert) {
        //         // clear alerts when an empty alert is received
        //         this.alerts = [];
        //         return;
        //     }
    
        //     // add alert to array
        //     this.alerts.push(alert);
            
        //     // remove alert after 5 seconds
        //     setTimeout(() => this.removeAlert(alert), 5000);
        // });
    }

    // subscribe to alerts
    getAlert(alertId?: string): Observable<any> {
        return this.subject.asObservable().filter((x: Alert) => x && x.alertId === alertId);
    }

    // convenience methods
    success(message: string) {
        this.alert(new Alert({ message, type: AlertType.Success }));
    }

    error(message: string) {
        this.alert(new Alert({ message, type: AlertType.Error }));
    }

    info(message: string) {
        this.alert(new Alert({ message, type: AlertType.Info }));
    }

    warn(message: string) {
        this.alert(new Alert({ message, type: AlertType.Warning }));
    }

    // main alert method    
    alert(alert: Alert) {
        this.keepAfterRouteChange = alert.keepAfterRouteChange;
        this.subject.next(alert);
    }

    // clear alerts
    clear(alertId?: string) {
        this.subject.next(new Alert({ alertId }));
    }
    // removeAlert(alert: Alert) {
    //     this.alerts = this.alerts.filter(x => x !== alert);
    // }
}