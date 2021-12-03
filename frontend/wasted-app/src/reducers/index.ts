import {combineReducers} from "redux"
import {location} from "./location"
import {restaurant} from "./restaurant"

export const reducer = combineReducers({restaurant, location})
