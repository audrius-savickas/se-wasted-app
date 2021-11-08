import {NavigationComponentProps} from "react-native-navigation"

export interface ProfileOwnProps {
    restaurantId: string,
    restaurantName: string
}

export interface ProfileProps extends NavigationComponentProps, ProfileOwnProps {}
