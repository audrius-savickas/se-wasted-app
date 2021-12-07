import {NavigationComponentProps} from "react-native-navigation"

export interface ProfileOwnProps {
  idRestaurant: string
}

export interface ProfileProps extends NavigationComponentProps, ProfileOwnProps {}
