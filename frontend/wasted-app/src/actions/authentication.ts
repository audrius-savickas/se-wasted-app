import {User} from "@react-native-google-signin/google-signin"
import {createAction} from "@reduxjs/toolkit"

export const setUser = createAction<User>("SET_USER")
