import {Injectable} from '@angular/core';

@Injectable({providedIn: 'root'})

export class PlayPlannerService {
const currentField: Field
  constructor() {
  }

  loadPlay(id: string){
    this.loadField()
  }
  loadField(id: string){

  }
  loadObjects(playId: string)
  {

  }
  addPlayer(playId: string, player: FieldObject)
  {

  }

}

export type FieldObjectType = 'player' | 'ball' | 'object';
interface FieldObject {id: string, x: number, y: number, type: FieldObjectType}
interface Player extends FieldObject {type: 'player', team: number, number: number, name: string}
interface Ball extends FieldObject {type: 'ball'}
interface Object extends FieldObject {type: 'object', width: number, height: number}
interface Field {id: string, width: number, height: number, objects: (Player | Ball | Object)[]}
