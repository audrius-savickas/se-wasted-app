import React, { useEffect } from "react"
import { Navigation } from "react-native-navigation"
import { Text, View, TextField, TextArea, Button } from "react-native-ui-lib"
import { TextFieldProps } from "react-native-ui-lib/generatedTypes/src/incubator"
import { setHomeRoot } from "../../../services/navigation"
import { AddFoodScreenProps } from "./interfaces"

const textFieldCommonValues : TextFieldProps = {
  editable:false,
  centered: false
}

export const AddFood = ({} : AddFoodScreenProps) => {
  
  useEffect(() => {
    const listener = Navigation.events().registerNavigationButtonPressedListener(({buttonId}) => {
      if (buttonId === "LOG_OUT") {
        setHomeRoot()
      }
    })
    return () => listener.remove()
  }, [])

  return (
      <View flex margin-s3>
        <Text center text40M marginT-10>My profile</Text>
        <View flex left marginT-10 marginL-10
          style={{
            flexDirection: "column",
            justifyContent: "center"
          }}
        >
          <TextField
            title="Name *"
            textFieldCommonValues
          />
          <Text>Description</Text>
          <TextArea />
          <TextField
            title="Image url"
            textFieldCommonValues
          />
        </View>
        <View 
          flex
          style={{
            flexDirection: "row",
            alignItems: 'center',
            justifyContent: 'space-evenly',
          }}
        >
          <Button
            link
            label="Reset"
          />
          <Button
            label="Save"
          />
        </View>
      </View>
  )
}
