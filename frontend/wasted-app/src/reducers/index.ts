import {combineReducers} from "redux"
import {authentication} from "./authentication"
import {location} from "./location"
import {restaurant} from "./restaurant"

export const reducer = combineReducers({restaurant, location, authentication})
