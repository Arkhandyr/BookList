import { EventEmitter } from '@angular/core';
import { Profile } from '../interfaces/Profile';

export class Emitters {
    static authEmitter = new EventEmitter<Profile>();
}