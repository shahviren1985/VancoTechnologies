import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Subject } from 'rxjs/Subject';

@Injectable()
export class EventBus {
    private _listners = new Subject<any>();

    on(): Observable<any> {
        return this._listners.asObservable();
    }

    emit(filterBy: any) {
        this._listners.next({ filterBy });
    }
}
