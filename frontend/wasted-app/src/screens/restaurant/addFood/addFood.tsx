import React, { useEffect, useState } from "react"
import { Navigation } from "react-native-navigation"
import { Text, View, TextField, TextArea, Button, Wizard, WizardStepStates, Colors } from "react-native-ui-lib"
import { TextFieldProps } from "react-native-ui-lib/generatedTypes/src/incubator"
import { setHomeRoot } from "../../../services/navigation"
import { AddFoodScreenProps } from "./interfaces"

const textFieldCommonValues : TextFieldProps = {
  editable:false,
  centered: false
}

const completedStepIndex = 3

export const AddFood = ({} : AddFoodScreenProps) => {
  
  const [activeIndex, setActiveIndex] = useState<number>(0)

  useEffect(() => {
    const listener = Navigation.events().registerNavigationButtonPressedListener(({buttonId}) => {
      if (buttonId === "LOG_OUT") {
        setHomeRoot()
      }
    })
    return () => listener.remove()
  }, [])

  const onActiveIndexChanged = () => {

  }

  const getStepState = (n: Number) => {
    if (n===activeIndex) return Wizard.States.ENABLED

    return (n>activeIndex)
      ? Wizard.States.DISABLED 
      : Wizard.States.COMPLETED
  }

  const renderCurrentStep = () => {
    return (
      <View flex left marginT-10 marginL-10
            style={{
              flexDirection: "column",
              justifyContent: "center"
            }}
      >
        {renderStep(activeIndex)}
      </View>
    )
  }

  const renderStep = (n: number) => {
    switch(n) {
      case 0:
        return (
          <>
          <TextField
            title="Name *"
            textFieldCommonValues
          />
          <TextField
            title="Image url"
            textFieldCommonValues
            />
          <Text>Description</Text>
          <TextArea />
          </>
        )
      case 1:
        return (
          <>
          </>
        )
      case 2:
        return (
          <>
          <View 
            flex
            style={{
              flexDirection: "row",
              alignItems: 'flex-end',
              width: '100%',
              justifyContent: 'space-evenly'
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
          </>
        )
    }
  }

  return (
      <View flex margin-s3>
        <Text center text40M marginT-10>Add new meal</Text>
        <Wizard testID={'addFood.wizard'} activeIndex={activeIndex} onActiveIndexChanged={onActiveIndexChanged}>
          <Wizard.Step state= {getStepState(0)} label={'Fill basic information'}/>
          <Wizard.Step state={getStepState(1)} label={'Fill price information'}/>
          <Wizard.Step state={getStepState(2)} label={'Add food'}/>
        </Wizard>

        {renderCurrentStep()}

        <View 
            flex
            style={{
              flexDirection: "row",
              alignItems: 'flex-end',
              width: '100%',
              justifyContent: 'space-evenly'
            }}
        >
          <Button
            backgroundColor={Colors.blue10}
            label="Previous step"
            disabled={activeIndex===0}
            onPress={() => setActiveIndex(activeIndex-1)}
          />
          <Button
            backgroundColor={Colors.green10}
            label="Next"
            disabled={activeIndex===completedStepIndex}
            onPress={() => setActiveIndex(activeIndex+1)}
          />
        </View>
      </View>
  )
}
